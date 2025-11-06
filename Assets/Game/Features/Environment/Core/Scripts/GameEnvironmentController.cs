using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Core
{
    public class GameEnvironmentController : ControllerWithResultBase
    {
        private readonly GameModel m_model;

        public GameEnvironmentController(IControllerFactory controllerFactory, GameModel model) : base(controllerFactory)
        {
            m_model = model;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            var args = new FieldControllerArgs(m_model.CurrentLevelVariant);
            ExecuteAndWaitResultAsync<ReferenceFieldController, FieldControllerArgs>(args, cancellationToken).Forget();
            ExecuteAndWaitResultAsync<InputFieldController, FieldControllerArgs>(args, cancellationToken).Forget();
        }
    }
}