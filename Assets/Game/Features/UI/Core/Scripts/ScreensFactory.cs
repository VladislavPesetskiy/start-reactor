using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.UI.Core.Scripts
{
    public class ScreensFactory
    {
        private readonly GameScreensConfig m_screensConfig;
        private readonly GameUIView m_uiView;

        public ScreensFactory(GameScreensConfig screensConfig, GameUIView uiView)
        {
            m_screensConfig = screensConfig;
            m_uiView = uiView;
        }
        
        public TScreenView CrateScreen<TScreenView>(bool showImmediately = false) where TScreenView : ScreenView
        {
            string screenName = typeof(TScreenView).FullName;
            ScreenData screenData = GetScreenData(screenName);
            if (screenData == null)
            {
                return null;
            }
            
            Assert.IsTrue(typeof(TScreenView).IsAssignableFrom(screenData.ScreenViewPrefab.GetType()), "Screen prefab type does not match the expected type");

            var screenView = Object.Instantiate(screenData.ScreenViewPrefab, m_uiView.PopupsContainer) as TScreenView;
            return screenView;
        }

        public ScreenData GetScreenData(string screenId)
        {
            if (string.IsNullOrWhiteSpace(screenId))
            {
                Debug.LogError($"Screen id can't be null or empty");
                return null;
            }
            
            ScreenData screenData = m_screensConfig.Screens.FirstOrDefault(t => t.ScreenId == screenId);
            if (screenData == null)
            {
                Debug.LogError($"Can't find screen with id {screenId}");
            }
            
            return screenData;
        }
    }
}