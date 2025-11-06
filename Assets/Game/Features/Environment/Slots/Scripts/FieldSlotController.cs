using Playtika.Controllers;

namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotController : ControllerWithResultBase<FieldSlotArgs, EmptyControllerResult>
    {
        private readonly FieldSlotFactory m_factory;
       
        protected FieldSlotView View { get; private set; }

        public FieldSlotController(IControllerFactory controllerFactory, FieldSlotFactory factory) : base(controllerFactory)
        {
            m_factory = factory;
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            View = m_factory.Create(Args.Parent, Args.SlotViewPrefab);
            View.SpriteRenderer.size = Args.SlotSize;
            View.Collider.size = Args.SlotSize;
            View.transform.localPosition = Args.LocalPosition;
        }
    }
}