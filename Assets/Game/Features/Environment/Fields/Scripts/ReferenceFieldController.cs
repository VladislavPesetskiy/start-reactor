using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Slots.Scripts;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class ReferenceFieldController : BaseFieldController
    {
        private readonly FieldFactory m_fieldFactory;
        private readonly GameVisualConfig m_visualConfig;

        public ReferenceFieldController
        (
            IControllerFactory controllerFactory,
            FieldFactory fieldFactory,
            GameVisualConfig visualConfig
        ) : base(controllerFactory)
        {
            m_fieldFactory = fieldFactory;
            m_visualConfig = visualConfig;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override FieldView CreateView()
        {
            ReferenceFieldView view = m_fieldFactory.CreateReferenceField(); 
            float slotSize = Args.LevelVariantData.FieldSizeX >= Args.LevelVariantData.FieldSizeY
                ? view.SlotsArea.x / Args.LevelVariantData.FieldSizeX
                : view.SlotsArea.y / Args.LevelVariantData.FieldSizeY;

            float fieldSizeX = slotSize * Args.LevelVariantData.FieldSizeX;
            float fieldSizeY = slotSize * Args.LevelVariantData.FieldSizeY;
            
            view.Background.size = new Vector2(fieldSizeX, fieldSizeY);
            return view;
        }

        protected override FieldSlotView GetSlotViewPrefab()
        {
            return m_visualConfig.ReferenceFieldSlotViewPrefab;
        }

        protected override void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken)
        {
            ExecuteAndWaitResultAsync<ReferenceFieldSlotController, FieldSlotArgs>(args, cancellationToken).Forget();
        }
    }
}