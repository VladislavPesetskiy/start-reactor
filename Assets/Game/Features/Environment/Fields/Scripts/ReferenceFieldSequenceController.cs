using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class ReferenceFieldSequenceController : ControllerWithResultBase
    {
        private readonly IFieldEventsRequestsModel m_requestsModel;
        private readonly GameModel m_gameModel;

        public ReferenceFieldSequenceController(IControllerFactory controllerFactory, IFieldEventsRequestsModel requestsModel, GameModel gameModel) : base(controllerFactory)
        {
            m_requestsModel = requestsModel;
            m_gameModel = gameModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(1f, cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            for (int i = 0; i < m_gameModel.ReferenceSequence.Length; i++)
            {
                Vector2 coord = m_gameModel.ReferenceSequence[i];
                m_requestsModel.RequestReferenceFieldSlotClick(coord);
                
                await UniTask.WaitForSeconds(0.5f, cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            }
            
            Complete();
        }
    }
}