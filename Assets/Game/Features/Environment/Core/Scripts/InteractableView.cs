using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Environment.Core
{
    public class InteractableView : MonoBehaviour, IPointerClickHandler
    {
        public event Action EventClicked;
        public bool IsInteractable { get; private set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnViewClicked();
        }

        public void SetInteractable(bool interactable)
        {
            IsInteractable =  interactable;
        }

        protected virtual void OnViewClicked()
        {
            if (IsInteractable == false)
            {
                return;
            }
            
            EventClicked?.Invoke();
        }
    }
}