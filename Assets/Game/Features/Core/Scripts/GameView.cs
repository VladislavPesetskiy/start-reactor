using Game.Environment.Core;
using Game.UI.Core.Scripts;
using UnityEngine;

namespace Game.Core
{
    public class GameView : MonoBehaviour
    {
        [field: SerializeField]
        public GameEnvironmentView GameEnvironmentView { get; private set; }
        
        [field: SerializeField]
        public GameUIView GameUIView { get; private set; }
    }
}