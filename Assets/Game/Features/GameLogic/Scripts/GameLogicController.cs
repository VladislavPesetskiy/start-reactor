using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;
using UnityEngine;

namespace Game.GameLogic.Scripts
{
    public class GameLogicController : ControllerWithResultBase
    {
        private readonly GameModel m_gameModel;
        private readonly IFieldEventsModel m_fieldEventsModel;
        private readonly IFieldEventsRequestsModel m_fieldRequestsModel;
        private readonly IGameEventsRequestsModel m_eventsRequestsModel;

        private UniTaskCompletionSource m_completionSource = new();

        public GameLogicController
        (
            IControllerFactory controllerFactory,
            GameModel gameModel,
            IFieldEventsModel fieldEventsModel,
            IFieldEventsRequestsModel fieldRequestsModel,
            IGameEventsRequestsModel eventsRequestsModel
        ) : base(controllerFactory)
        {
            m_gameModel = gameModel;
            m_fieldEventsModel = fieldEventsModel;
            m_fieldRequestsModel = fieldRequestsModel;
            m_eventsRequestsModel = eventsRequestsModel;
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_fieldEventsModel.EventInputFieldSlotClick += OnInputFieldSlotClicked;
        }

        protected override void OnStop()
        {
            base.OnStop();

            m_fieldEventsModel.EventInputFieldSlotClick -= OnInputFieldSlotClicked;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            m_gameModel.SetFieldInputEnabled(false);
            m_fieldRequestsModel.RequestFieldInteractableChanged();
            m_fieldRequestsModel.RequestUpdateInteractableView();
            
            while (cancellationToken.IsCancellationRequested == false)
            {
                await ExecuteAndWaitResultAsync<ReferenceFieldSequenceController>(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                await UniTask.WaitForSeconds(0.5f, cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                
                m_gameModel.SetFieldInputEnabled(true);
                m_fieldRequestsModel.RequestFieldInteractableChanged();
                m_fieldRequestsModel.RequestUpdateInteractableView();
                
                await m_completionSource.Task;
                cancellationToken.ThrowIfCancellationRequested();
                
                m_gameModel.SetFieldInputEnabled(false);
                m_fieldRequestsModel.RequestFieldInteractableChanged();
                
                await UniTask.WaitForSeconds(0.25f, cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                m_fieldRequestsModel.RequestUpdateInteractableView();
                m_completionSource = new UniTaskCompletionSource();
            }
        }

        private void OnInputFieldSlotClicked(Vector2 coord)
        {
            if (m_gameModel.IsCorrectSlot(coord))
            {
                IncreaseIterationPointer();
            }
            else
            {
                m_gameModel.ResetIterationPointer();
                m_completionSource.TrySetResult();
            }
        }

        private void IncreaseIterationPointer()
        {
            m_gameModel.IncreaseIterationPointer();
            if (m_gameModel.IsInputSequenceFilled())
            {
                IncreaseIteration();
            }
        }

        private void IncreaseIteration()
        {
            m_gameModel.IncreaseIterationIndex();
            m_gameModel.ResetIterationPointer();
                    
            if (m_gameModel.IsIterationsCompleted())
            {
                m_eventsRequestsModel.RequestWin();
                m_gameModel.SetFieldInputEnabled(false);
                m_fieldRequestsModel.RequestFieldInteractableChanged();
            }
            else
            {
                m_completionSource.TrySetResult();
            }
        }
    }
}