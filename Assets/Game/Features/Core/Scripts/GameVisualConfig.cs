using Game.Environment.Fields;
using Game.Environment.Slots.Scripts;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "Configs/Visual/" + nameof(GameVisualConfig), fileName = nameof(GameVisualConfig), order = 0)]
    public class GameVisualConfig : ScriptableObject
    {
        [field: SerializeField]
        public FieldView InputFieldViewPrefab { get; private set; }
        
        [field: SerializeField]
        public FieldSlotView InputFieldSlotViewPrefab { get; private set; }
        
        [field: SerializeField]
        public ReferenceFieldView ReferenceFieldViewPrefab { get; private set; }
        
        [field: SerializeField]
        public FieldSlotView ReferenceFieldSlotViewPrefab { get; private set; }
    }
}