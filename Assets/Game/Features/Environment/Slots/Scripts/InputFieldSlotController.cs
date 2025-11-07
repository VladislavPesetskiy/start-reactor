using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Slots.Scripts
{
    public class InputFieldSlotController : FieldSlotController
    {
        private readonly IFieldEventsRequestsModel m_requestsModel;
        private readonly GameModel m_gameModel;
        private UniTaskCompletionSource m_slotSelectedSource = new();

        public InputFieldSlotController
        (
            IControllerFactory controllerFactory,
            FieldFactory factory,
            IFieldEventsRequestsModel requestsModel,
            GameModel gameModel
        ) : base(controllerFactory, factory)
        {
            m_requestsModel = requestsModel;
            m_gameModel = gameModel;
        }

        protected override void OnStart()
        {
            base.OnStart();

            View.EventClicked += OnSlotClicked;
        }

        protected override void OnStop()
        {
            base.OnStop();

            View.EventClicked -= OnSlotClicked;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var args = new FieldSlotInteractableControllerArgs(View);
            ExecuteAndWaitResultAsync<FieldSlotInteractableController, FieldSlotInteractableControllerArgs>(args, cancellationToken).Forget();
            
            while (cancellationToken.IsCancellationRequested == false)
            {
                await m_slotSelectedSource.Task;
                cancellationToken.ThrowIfCancellationRequested();

                bool isCorrectSlot = m_gameModel.IsCorrectSlot(Args.Coord);
                if (isCorrectSlot)
                {
                    await View.PlaySelectSlotAnimation(cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                    await View.PlayUnselectSlotAnimation(cancellationToken);
                }
                else
                {
                    await View.PlayStartMistakeSlotAnimation(cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                    await View.PlayEndMistakeSlotAnimation(cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                m_slotSelectedSource = new UniTaskCompletionSource();
            }
        }

        private void OnSlotClicked()
        {
            m_slotSelectedSource.TrySetResult();
            m_requestsModel.RequestInputFieldSlotClick(Args.Coord);
        }
    }
}