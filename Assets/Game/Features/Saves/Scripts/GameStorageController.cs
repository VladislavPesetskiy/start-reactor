using Cysharp.Threading.Tasks;
using Game.Core;
using Playtika.Controllers;

namespace Game.Saves.Scripts
{
    public class GameStorageController : ControllerBase
    {
        private readonly IGameEventsModel m_gameEventsModel;
        private readonly GameStorageDataModel m_storageDataModel;

        public GameStorageController(IControllerFactory controllerFactory, IGameEventsModel gameEventsModel, GameStorageDataModel storageDataModel) : base(controllerFactory)
        {
            m_gameEventsModel = gameEventsModel;
            m_storageDataModel = storageDataModel;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            m_gameEventsModel.EventWin += OnLevelCompleted;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_gameEventsModel.EventWin -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            m_storageDataModel.IncreaseLevelIndex();
            ExecuteAndWaitResultAsync<GameSaveStorageController>(CancellationToken).Forget();
        }
    }
}