using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotInteractableController : ControllerWithResultBase<FieldSlotInteractableControllerArgs, EmptyControllerResult>
    {
        private readonly IFieldEventsModel m_fieldEventsModel;
        private readonly GameModel m_gameModel;
        private UniTaskCompletionSource m_completionSource = new();

        public FieldSlotInteractableController(IControllerFactory controllerFactory, IFieldEventsModel fieldEventsModel, GameModel gameModel) : base(controllerFactory)
        {
            m_fieldEventsModel = fieldEventsModel;
            m_gameModel = gameModel;
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_fieldEventsModel.EventInteractableChanged += OnInteractableChanged;
            m_fieldEventsModel.EventUpdateInteractableView += OnInteractableViewChanged;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_fieldEventsModel.EventInteractableChanged -= OnInteractableChanged;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                await m_completionSource.Task;
                cancellationToken.ThrowIfCancellationRequested();

                if (m_gameModel.IsFieldInteractableEnabled)
                {
                    await Args.View.PlayEnableInteractableAnimation(cancellationToken);
                }
                else
                {
                    await Args.View.PlayDisableInteractableAnimation(cancellationToken);
                }
                
                cancellationToken.ThrowIfCancellationRequested();
                m_completionSource = new UniTaskCompletionSource();
            }
        }

        private void OnInteractableChanged()
        {
            Args.View.SetInteractable(m_gameModel.IsFieldInteractableEnabled);
        }

        private void OnInteractableViewChanged()
        {
            m_completionSource.TrySetResult();
        }
    }
}