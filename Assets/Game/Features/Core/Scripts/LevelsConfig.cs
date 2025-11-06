using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "Configs/Levels/" + nameof(LevelsConfig), fileName = nameof(LevelsConfig), order = 0)]
    public class LevelsConfig : ScriptableObject
    {
        [field: SerializeField]
        public LevelsRepeatType RepeatType { get; private set; }
        
        [field: SerializeField]
        public LevelVariantData[] LevelVariants { get; private set; }
    }
}