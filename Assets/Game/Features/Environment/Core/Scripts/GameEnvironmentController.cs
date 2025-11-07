using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Core
{
    public class GameEnvironmentController : ControllerBase
    {
        private readonly GameModel m_model;

        public GameEnvironmentController(IControllerFactory controllerFactory, GameModel model) : base(controllerFactory)
        {
            m_model = model;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            var args = new FieldControllerArgs(m_model.CurrentLevelVariant);
            ExecuteAndWaitResultAsync<ReferenceFieldController, FieldControllerArgs>(args, CancellationToken).Forget();
            ExecuteAndWaitResultAsync<InputFieldController, FieldControllerArgs>(args, CancellationToken).Forget();
        }
    }
}