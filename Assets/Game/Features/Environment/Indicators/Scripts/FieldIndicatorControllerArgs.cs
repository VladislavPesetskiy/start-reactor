using UnityEngine;

namespace Game.Environment.Indicators
{
    public struct FieldIndicatorControllerArgs
    {
        public readonly int IndicatorIndex;
        public readonly Vector2 LocalPosition;
        public readonly FieldIndicatorView ViewPrefab;
        public readonly Transform ViewParent;
        
        public FieldIndicatorControllerArgs(int indicatorIndex, Vector2 localPosition, FieldIndicatorView viewPrefab, Transform viewParent)
        {
            IndicatorIndex = indicatorIndex;
            LocalPosition = localPosition;
            ViewPrefab = viewPrefab;
            ViewParent = viewParent;
        }
    }
}