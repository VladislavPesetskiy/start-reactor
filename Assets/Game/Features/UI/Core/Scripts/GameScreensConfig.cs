using UnityEngine;

namespace Game.UI.Core.Scripts
{
    [CreateAssetMenu(menuName = "Configs/UI/" + nameof(GameScreensConfig), fileName = nameof(GameScreensConfig), order = 0)]
    public class GameScreensConfig : ScriptableObject
    {
        [field: SerializeField]
        public ScreenData[] Screens { get; private set; }
        
        
    }
}