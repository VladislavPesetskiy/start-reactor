using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Indicators;
using Game.Environment.Slots.Scripts;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class ReferenceFieldController : BaseFieldController
    {
        private readonly FieldFactory m_fieldFactory;
        private readonly FieldVisualProvider m_visualProvider;
        private readonly GameModel m_gameModel;

        public ReferenceFieldController
        (
            IControllerFactory controllerFactory,
            FieldFactory fieldFactory,
            FieldVisualProvider visualProvider,
            GameModel gameModel
        ) : base(controllerFactory)
        {
            m_fieldFactory = fieldFactory;
            m_visualProvider = visualProvider;
            m_gameModel = gameModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            FieldIndicatorView indicatorViewPrefab = await m_visualProvider.GetFieldIndicatorPrefabAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            int iterationsCount = m_gameModel.CurrentLevelVariant.Iterations.Length;
            for (int i = 0; i < iterationsCount; i++)
            {
                float indicatorWidth = View.IndicatorsArea.x / iterationsCount;
                float horizontalOffset = indicatorWidth * iterationsCount * 0.5f - indicatorWidth * 0.5f;
                var localPosition = new Vector2(indicatorWidth * i - horizontalOffset, 0f);
                
                var indicatorArgs = new FieldIndicatorControllerArgs(i, localPosition, indicatorViewPrefab, View.IndicatorsContainer);
                Execute<ReferenceFieldIndicatorController, FieldIndicatorControllerArgs>(indicatorArgs);
            }
        }

        protected override async UniTask<FieldView> CreateView(CancellationToken cancellationToken)
        {
            ReferenceFieldView view = await m_fieldFactory.CreateReferenceFieldView(cancellationToken); 
            cancellationToken.ThrowIfCancellationRequested();
            
            float slotSize = Args.LevelVariantData.FieldSizeX >= Args.LevelVariantData.FieldSizeY
                ? view.SlotsArea.x / Args.LevelVariantData.FieldSizeX
                : view.SlotsArea.y / Args.LevelVariantData.FieldSizeY;

            float fieldSizeX = slotSize * Args.LevelVariantData.FieldSizeX;
            float fieldSizeY = slotSize * Args.LevelVariantData.FieldSizeY;
            
            view.Background.size = new Vector2(fieldSizeX, fieldSizeY);
            return view;
        }

        protected override async UniTask<FieldSlotView> GetSlotViewPrefab(CancellationToken cancellationToken)
        {
            return await m_visualProvider.GetReferenceFieldSlotPrefabAsync(cancellationToken);
        }

        protected override void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken)
        {
            ExecuteAndWaitResultAsync<ReferenceFieldSlotController, FieldSlotArgs>(args, cancellationToken).Forget();
        }
    }
}