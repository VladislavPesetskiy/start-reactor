using Game.Core;
using Game.Environment.Fields;
using Playtika.Controllers;

namespace Game.Environment.Indicators
{
    public class ReferenceFieldIndicatorController : ControllerBase<FieldIndicatorControllerArgs>
    {
        private readonly GameModel m_gameModel;
        private readonly FieldFactory m_fieldFactory;
        private FieldIndicatorView m_view;

        public ReferenceFieldIndicatorController(IControllerFactory controllerFactory, GameModel gameModel, FieldFactory fieldFactory) : base(controllerFactory)
        {
            m_gameModel = gameModel;
            m_fieldFactory = fieldFactory;
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_view = m_fieldFactory.CreateFieldIndicatorView(Args.ViewPrefab, Args.ViewParent);
            m_view.transform.localPosition = Args.LocalPosition;
            
            m_gameModel.EventIterationChanged += OnIterationChanged;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_gameModel.EventIterationChanged -= OnIterationChanged;
        }

        private void OnIterationChanged(int iteration)
        {
            if (Args.IndicatorIndex < iteration)
            {
                m_view.SetActiveState();
            }
            else
            {
                m_view.SetDefaultState();
            }
        }
    }
}