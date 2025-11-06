using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Core.Scripts
{
    public class GameWinScreenView : BaseAnimatedScreenView
    {
        [SerializeField]
        private Button m_nextButton;

        public event Action EventNextButtonClick;
        
        private void Awake()
        {
            m_nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            EventNextButtonClick?.Invoke();
        }
    }
}