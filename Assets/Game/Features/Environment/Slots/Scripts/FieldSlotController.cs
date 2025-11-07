using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotController : ControllerWithResultBase<FieldSlotArgs, EmptyControllerResult>
    {
        private readonly FieldFactory m_factory;
       
        protected FieldSlotView View { get; private set; }

        public FieldSlotController(IControllerFactory controllerFactory, FieldFactory factory) : base(controllerFactory)
        {
            m_factory = factory;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            View = m_factory.CreateFieldSlotView(Args.SlotViewPrefab, Args.Parent);
            View.SpriteRenderer.size = Args.SlotSize;
            View.Collider.size = Args.SlotSize;
            View.transform.localPosition = Args.LocalPosition;
        }
    }
}