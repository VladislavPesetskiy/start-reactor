using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Environment.Fields;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Slots.Scripts
{
    public class ReferenceFieldSlotController : FieldSlotController
    {
        private readonly IFieldEventsModel m_eventsModel;
        private UniTaskCompletionSource m_slotSelectedSource = new();

        public ReferenceFieldSlotController(IControllerFactory controllerFactory, FieldSlotFactory factory, IFieldEventsModel eventsModel) : base(controllerFactory, factory)
        {
            m_eventsModel = eventsModel;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            m_eventsModel.EventReferenceFieldSlotSelection += OnSlotSelected;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_eventsModel.EventReferenceFieldSlotSelection -= OnSlotSelected;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                await m_slotSelectedSource.Task;
                cancellationToken.ThrowIfCancellationRequested();
                
                await View.PlaySelectSlotAnimation(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                
                await View.PlayUnselectSlotAnimation(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                m_slotSelectedSource = new UniTaskCompletionSource();
            }
        }

        private void OnSlotSelected(Vector2 coord)
        {
            if (Args.Coord != coord)
            {
                return;
            }

            m_slotSelectedSource.TrySetResult();
        }
    }
}