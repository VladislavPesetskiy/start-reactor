using System.Threading;
using Cysharp.Threading.Tasks;
using Playtika.Controllers;

namespace Game.Saves.Scripts
{
    public class GameSaveStorageController : ControllerWithResultBase
    {
        private readonly GameStorageDataModel m_storageDataModel;

        public GameSaveStorageController(IControllerFactory controllerFactory, GameStorageDataModel storageDataModel) : base(controllerFactory)
        {
            m_storageDataModel = storageDataModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await m_storageDataModel.Save(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            Complete();
        }
    }
}