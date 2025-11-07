using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Indicators;
using Game.Environment.Slots.Scripts;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class InputFieldController : BaseFieldController
    {
        private readonly FieldFactory m_factory;
        private readonly FieldVisualProvider m_visualProvider;
        private readonly GameModel m_gameModel;

        public InputFieldController
        (
            IControllerFactory controllerFactory,
            FieldFactory factory,
            FieldVisualProvider visualProvider,
            GameModel gameModel
        ) : base(controllerFactory)
        {
            m_factory = factory;
            m_visualProvider = visualProvider;
            m_gameModel = gameModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            await base.OnFlowAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            FieldIndicatorView indicatorViewPrefab = await m_visualProvider.GetFieldIndicatorPrefabAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            int maxIndicatorsCount = m_gameModel.CurrentLevelVariant.Iterations.Max();
            for (int i = 0; i < maxIndicatorsCount; i++)
            {
                float indicatorWidth = View.IndicatorsArea.x / maxIndicatorsCount;
                float horizontalOffset = indicatorWidth * maxIndicatorsCount * 0.5f - indicatorWidth * 0.5f;
                var localPosition = new Vector2(indicatorWidth * i - horizontalOffset, 0f);
                
                var indicatorArgs = new FieldIndicatorControllerArgs(i, localPosition, indicatorViewPrefab, View.IndicatorsContainer);
                Execute<InputFieldIndicatorController, FieldIndicatorControllerArgs>(indicatorArgs);
            }
        }

        protected override async UniTask<FieldView> CreateView(CancellationToken cancellationToken)
        {
            FieldView view = await m_factory.CreateInputFieldView(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            return view;
        }

        protected override async UniTask<FieldSlotView> GetSlotViewPrefab(CancellationToken cancellationToken)
        {
            return await m_visualProvider.GetInputFieldSlotPrefabAsync(cancellationToken);
        }

        protected override void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken)
        {
            ExecuteAndWaitResultAsync<InputFieldSlotController, FieldSlotArgs>(args, cancellationToken).Forget();
        }
    }
}