using UnityEngine;

namespace Game.Environment.Indicators
{
    public class FieldIndicatorView : MonoBehaviour
    {
        [field: SerializeField]
        public SpriteRenderer SpriteRenderer { get; private set; }

        [SerializeField]
        private Sprite m_defaultStateSprite;
        
        [SerializeField]
        private Sprite m_activeStateSprite;

        public void SetActiveState()
        {
            SpriteRenderer.sprite = m_activeStateSprite;
        }

        public void SetDefaultState()
        {
            SpriteRenderer.sprite = m_defaultStateSprite;
        }
    }
}