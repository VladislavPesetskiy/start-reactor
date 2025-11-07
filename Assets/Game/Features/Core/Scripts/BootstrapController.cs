using Cysharp.Threading.Tasks;
using Game.Saves.Scripts;
using Game.UI.Core.Scripts;
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
            await ExecuteAndWaitResultAsync<GameLoadStorageController>(CancellationToken);
            CancellationToken.ThrowIfCancellationRequested();
            
            while (CancellationToken.IsCancellationRequested == false)
            {
                await ExecuteAndWaitResultAsync<GameMenuScreenController>(CancellationToken);
                CancellationToken.ThrowIfCancellationRequested();
                
                await ExecuteAndWaitResultAsync<GameLoopController>(CancellationToken);
                CancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}