using UnityEngine;

namespace Game.Environment.Core
{
    public class GameEnvironmentView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform ReferenceFieldPoint { get; private set; }
        
        [field: SerializeField]
        public Transform InputFieldPoint { get; private set; }
    }
}