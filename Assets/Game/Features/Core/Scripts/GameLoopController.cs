using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Environment.Core;
using Game.GameLogic.Scripts;
using Game.Saves.Scripts;
using Game.UI.Core.Scripts;
using Playtika.Controllers;

namespace Game.Core
{
    public class GameLoopController : ControllerWithResultBase
    {
        private readonly GameModel m_gameModel;
        private readonly IGameEventsModel m_gameEventsModel;

        private UniTaskCompletionSource m_completionSource = new();

        public GameLoopController(IControllerFactory controllerFactory, GameModel gameModel, IGameEventsModel gameEventsModel) : base(controllerFactory)
        {
            m_gameModel = gameModel;
            m_gameEventsModel = gameEventsModel;
        }

        protected override void OnStart()
        {
            m_gameEventsModel.EventRestart += OnEventRestart;
        }

        protected override void OnStop()
        {
            m_gameEventsModel.EventRestart -= OnEventRestart;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            Execute<GameStorageController>();
            m_gameModel.Initialize();
            
            Execute<GameUIController>();
            ExecuteAndWaitResultAsync<GameLogicController>(CancellationToken).Forget();

            await m_completionSource.Task;
            cancellationToken.ThrowIfCancellationRequested();
            
            Complete();
        }

        private void OnEventRestart()
        {
            m_completionSource.TrySetResult();
        }
    }
}