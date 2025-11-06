using Game.UI.Core.Scripts;
using UnityEditor;
using UnityEngine;

namespace Game.UI.Core
{
    [CustomEditor(typeof(GameScreensConfig))]
    public class GameScreenConfigsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Refresh screens ids"))
            {
                var config = (GameScreensConfig)target;
                foreach (ScreenData screen in config.Screens)
                {
                    screen.SetScreenIdByPrefabName();
                }
                
                EditorUtility.SetDirty(config);
            }
        }   
    }
}