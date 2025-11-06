using UnityEngine;

namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotFactory
    {
        public FieldSlotView Create(Transform parent, FieldSlotView slotViewPrefab)
        {
            FieldSlotView instance = Object.Instantiate(slotViewPrefab, parent);
            return instance;
        }
    }
}