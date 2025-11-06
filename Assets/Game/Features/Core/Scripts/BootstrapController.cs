using Cysharp.Threading.Tasks;
using Playtika.Controllers;

namespace Game.Core
{
    public class BootstrapController : RootController
    {
        public BootstrapController(IControllerFactory controllerFactory)
            : base(controllerFactory)
        {
        }

        protected override void OnStart()
        {
            FlowAsync().Forget();

            base.OnStart();
        }

        private async UniTask FlowAsync()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                await ExecuteAndWaitResultAsync<GameLoopController>(CancellationToken);
            }
        }
    }
}