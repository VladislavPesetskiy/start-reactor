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
        protected virtual float OffsetBetweenSlotsPercent => 0.05f;
        
        public BaseFieldController(IControllerFactory controllerFactory) : base(controllerFactory)
        {
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            Object.Destroy(View.gameObject);
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            View = await CreateView(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            FieldSlotView slotViewPrefab = await GetSlotViewPrefab(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            float totalWidth = View.SlotsArea.x;
            float totalHeight = View.SlotsArea.y;
            int slotsX = Args.LevelVariantData.FieldSizeX;
            int slotsY = Args.LevelVariantData.FieldSizeY;

            float spacingX = OffsetBetweenSlotsPercent * totalWidth / (slotsX - 1 > 0 ? slotsX - 1 : 1);
            float spacingY = OffsetBetweenSlotsPercent * totalHeight / (slotsY - 1 > 0 ? slotsY - 1 : 1);

            float spacing = slotsX > slotsY ? spacingX : spacingY;
            spacingX = spacingY = spacing;

            float slotSizeX = (totalWidth - spacingX * (slotsX - 1)) / slotsX;
            float slotSizeY = (totalHeight - spacingY * (slotsY - 1)) / slotsY;

            float slotSize = Mathf.Min(slotSizeX, slotSizeY);

            float totalSlotsWidth = slotSize * slotsX + spacingX * (slotsX - 1);
            float totalSlotsHeight = slotSize * slotsY + spacingY * (slotsY - 1);

            float offsetX = totalSlotsWidth * 0.5f - slotSize * 0.5f;
            float offsetY = totalSlotsHeight * 0.5f - slotSize * 0.5f;
            var offset = new Vector2(offsetX, offsetY);
            
            for (int y = 0; y < slotsY; y++)
            {
                for (int x = 0; x < slotsX; x++)
                {
                    Vector2 coord = new Vector2(x, y);
                    Vector2 localPosition = new Vector2(x * (slotSize + spacingX), y * (slotSize + spacingY)) - offset;
                    Vector2 spriteSize = Vector2.one * slotSize;
                    
                    var slotArgs = new FieldSlotArgs(slotViewPrefab, View.SlotsContainer, coord, localPosition, spriteSize);
                    ExecuteSlotController(slotArgs, cancellationToken);
                }
            }
        }

        protected abstract UniTask<FieldView> CreateView(CancellationToken cancellationToken);
        protected abstract UniTask<FieldSlotView> GetSlotViewPrefab(CancellationToken cancellationToken);
        protected abstract void ExecuteSlotController(FieldSlotArgs args, CancellationToken cancellationToken);
    }
}