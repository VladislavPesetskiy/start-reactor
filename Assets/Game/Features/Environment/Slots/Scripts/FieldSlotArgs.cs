using UnityEngine;

namespace Game.Environment.Slots.Scripts
{
    public struct FieldSlotArgs
    {
        public FieldSlotView SlotViewPrefab { get; }
        public Transform Parent { get; }
        public Vector2 Coord { get; }
        public Vector2 LocalPosition { get; }
        public Vector2 SlotSize { get; }

        public FieldSlotArgs(FieldSlotView slotViewPrefab, Transform parent, Vector2 coord, Vector2 localPosition, Vector2 slotSize)
        {
            SlotViewPrefab = slotViewPrefab;
            Parent = parent;
            Coord = coord;
            LocalPosition = localPosition;
            SlotSize = slotSize;
        }
    }
}