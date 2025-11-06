using UnityEngine;

namespace Game.Environment.Fields
{
    public class FieldView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform SlotsContainer { get; private set; }
        
        [field: SerializeField]
        public Transform IndicatorsContainer { get; private set; }
        
        [field: SerializeField]
        public Vector2 SlotsArea { get; private set; }
        
        [field: SerializeField]
        public Vector2 IndicatorsArea { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(SlotsContainer.position, SlotsArea);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(IndicatorsContainer.position, IndicatorsArea);
        }
    }
}