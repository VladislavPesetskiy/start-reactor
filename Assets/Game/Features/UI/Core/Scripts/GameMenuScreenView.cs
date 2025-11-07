using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Core.Scripts
{
    public class GameMenuScreenView : BaseAnimatedScreenView
    {
        [SerializeField]
        private Button m_startButton;

        [SerializeField]
        private TextMeshProUGUI m_levelNumberSource;
        
        [SerializeField]
        private string m_levelNumberFormat = "Level {0}";
        
        public event Action EventStartButtonClick;

        public void SetLevelNumber(int levelNumber)
        {
            m_levelNumberSource.text = string.Format(m_levelNumberFormat, levelNumber);
        }
        
        private void Awake()
        {
            m_startButton.onClick.AddListener(OnStartButtonClick);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_startButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            EventStartButtonClick?.Invoke();
        }
    }
}