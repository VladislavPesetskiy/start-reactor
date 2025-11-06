using UnityEngine;

namespace Game.UI.Core.Scripts
{
    public class GameUIView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform PopupsContainer { get; private set; }
    }
}