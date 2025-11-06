using Cysharp.Threading.Tasks;
using Game.Core;
using Playtika.Controllers;

namespace Game.UI.Core.Scripts
{
    public class GameUIController : ControllerBase
    {
        private readonly IGameEventsModel m_gameEventsModel;

        public GameUIController(IControllerFactory controllerFactory, IGameEventsModel gameEventsModel) : base(controllerFactory)
        {
            m_gameEventsModel = gameEventsModel;
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
            ExecuteAndWaitResultAsync<GameWinScreenController>(CancellationToken).Forget();
        }
    }
}