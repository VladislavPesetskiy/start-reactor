using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Slots.Scripts;
using Playtika.Controllers;

namespace Game.Environment.Fields
{
    public class InputFieldController : BaseFieldController
    {
        private readonly FieldFactory m_factory;
        private readonly GameVisualConfig m_visualConfig;

        public InputFieldController(IControllerFactory controllerFactory, FieldFactory factory, GameVisualConfig visualConfig) : base(controllerFactory)
        {
            m_factory = factory;
            m_visualConfig = visualConfig;
        }

        protected override FieldView CreateView()
        {
            FieldView view = m_factory.CreateInputField();
            return view;
        }

        protected override FieldSlotView GetSlotViewPrefab()
        {
            return m_visualConfig.InputFieldSlotViewPrefab;
        }

        protected override void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken)
        {
            ExecuteAndWaitResultAsync<InputFieldSlotController, FieldSlotArgs>(args, cancellationToken).Forget();
        }
    }
}