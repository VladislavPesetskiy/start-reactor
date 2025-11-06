using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Extensions;
using UnityEngine;

namespace Game.UI.Core.Scripts
{
    public abstract class BaseAnimatedScreenView : ScreenView
    {
        [SerializeField]
        private CanvasGroup m_canvasGroup;
        
        [SerializeField]
        private float m_showFadeDuration = 0.5f;
        
        [SerializeField]
        private float m_hideFadeDuration = 0.5f;
        
        public virtual async UniTask PlayShowAnimation(CancellationToken cancellationToken)
        {
            m_canvasGroup.alpha = 0f;
            
            await m_canvasGroup
                .DOFade(1f,  m_showFadeDuration).SetId(this)
                .ToUniTask(cancellationToken);
        }

        public virtual async UniTask PlayHideAnimation(CancellationToken cancellationToken)
        {
            await m_canvasGroup
                .DOFade(0f,  m_showFadeDuration).SetId(this)
                .ToUniTask(cancellationToken);
        }

        protected virtual void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}