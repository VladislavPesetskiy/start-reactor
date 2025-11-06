using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Environment.Core;
using Game.Extensions;
using UnityEngine;

namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotView : InteractableView
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        [field: SerializeField]
        public BoxCollider2D Collider { get; private set; }

        [Header("Animations")]
        [SerializeField]
        private float m_selectSlotAnimationDuration = 0.5f;
        
        [SerializeField]
        private Color m_selectSlotAnimationColor =  Color.white;
        
        [SerializeField]
        private float m_unselectSlotAnimationDuration = 0.5f;
        
        [SerializeField]
        private Color m_unselectSlotAnimationColor =  Color.white;
        
        [SerializeField]
        private float m_startMistakeSlotAnimationDuration = 0.5f;
        
        [SerializeField]
        private Color m_startMistakeSlotAnimationColor =  Color.white;
        
        [SerializeField]
        private float m_endMistakeSlotAnimationDuration = 0.5f;
        
        [SerializeField]
        private Color m_endMistakeSlotAnimationColor =  Color.white;
        
        [SerializeField]
        private float m_enableInteractableAnimationDuration = 0.25f;
        
        [SerializeField]
        private Color m_enableInteractableAnimationColor = Color.white;
        
        [SerializeField]
        private float m_disableInteractableAnimationDuration = 0.25f;
        
        [SerializeField]
        private Color m_disableInteractableAnimationColor = Color.white;
        
        public async UniTask PlaySelectSlotAnimation(CancellationToken cancellationToken)
        {
           await SpriteRenderer
                .DOColor(m_selectSlotAnimationColor, m_selectSlotAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }

        public async UniTask PlayUnselectSlotAnimation(CancellationToken cancellationToken)
        {
            await SpriteRenderer
                .DOColor(m_unselectSlotAnimationColor, m_unselectSlotAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }

        public async UniTask PlayStartMistakeSlotAnimation(CancellationToken cancellationToken)
        {
            await SpriteRenderer
                .DOColor(m_startMistakeSlotAnimationColor, m_startMistakeSlotAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }

        public async UniTask PlayEndMistakeSlotAnimation(CancellationToken cancellationToken)
        {
            await SpriteRenderer
                .DOColor(m_endMistakeSlotAnimationColor, m_endMistakeSlotAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }

        public async UniTask PlayEnableInteractableAnimation(CancellationToken cancellationToken)
        {
            await SpriteRenderer
                .DOColor(m_enableInteractableAnimationColor, m_enableInteractableAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }
        
        public async UniTask PlayDisableInteractableAnimation(CancellationToken cancellationToken)
        {
            await SpriteRenderer
                .DOColor(m_disableInteractableAnimationColor, m_disableInteractableAnimationDuration)
                .SetId(this)
                .ToUniTask(cancellationToken);
        }
        
        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}