using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Environment.Slots.Scripts;
using Playtika.Controllers;
using UnityEngine;

namespace Game.Environment.Fields
{
    public abstract class BaseFieldController : ControllerWithResultBase<FieldControllerArgs, EmptyControllerResult>
    {
        protected FieldView View { get; private set; }
        
        public BaseFieldController(IControllerFactory controllerFactory) : base(controllerFactory)
        {
        }
        
        protected override UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            View = CreateView();
            
            float slotSize = Args.LevelVariantData.FieldSizeX >= Args.LevelVariantData.FieldSizeY
                ? View.SlotsArea.x / Args.LevelVariantData.FieldSizeX
                : View.SlotsArea.y / Args.LevelVariantData.FieldSizeY;

            float offsetX = slotSize * Args.LevelVariantData.FieldSizeX * 0.5f - slotSize * 0.5f;
            float offsetY = slotSize * Args.LevelVariantData.FieldSizeY * 0.5f - slotSize * 0.5f;
            var offset = new Vector2(offsetX, offsetY);

            FieldSlotView slotViewPrefab = GetSlotViewPrefab();
            
            for (int y = 0; y < Args.LevelVariantData.FieldSizeY; y++)
            {
                for (int x = 0; x < Args.LevelVariantData.FieldSizeX; x++)
                {
                    Vector2 coord = new Vector2(x, y);
                    Vector2 localPosition = new Vector2(slotSize * x, slotSize * y) - offset;
                    Vector2 spriteSize = Vector2.one * slotSize;
                    
                    var slotArgs = new FieldSlotArgs(slotViewPrefab, View.SlotsContainer, coord, localPosition, spriteSize);
                    ExecuteSlotController(slotArgs, cancellationToken);
                }
            }
            
            return base.OnFlowAsync(cancellationToken);
        }

        protected abstract FieldView CreateView();
        protected abstract FieldSlotView GetSlotViewPrefab();
        protected abstract void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken);
    }
}