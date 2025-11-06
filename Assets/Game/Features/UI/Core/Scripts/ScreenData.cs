using System;
using UnityEngine;

namespace Game.UI.Core.Scripts
{
    [Serializable]
    public class ScreenData
    {
        [field: SerializeField]
        public ScreenView ScreenViewPrefab { get; private set; }
        
        [field: SerializeField]
        public string ScreenId { get; private set; }

        public void SetScreenIdByPrefabName()
        {
            ScreenId = ScreenViewPrefab.GetType().FullName;
        }
    }
}