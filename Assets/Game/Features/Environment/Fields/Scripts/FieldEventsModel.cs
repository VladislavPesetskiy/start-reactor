using System;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class FieldEventsModel : IFieldEventsModel, IFieldEventsRequestsModel
    {
        public event Action<Vector2> EventInputFieldSlotClick;
        public event Action<Vector2> EventReferenceFieldSlotSelection;
        public event Action EventInteractableChanged;
        public event Action EventUpdateInteractableView;

        public void RequestReferenceFieldSlotClick(Vector2 slotCoord)
        {
            EventReferenceFieldSlotSelection?.Invoke(slotCoord);
        }

        public void RequestInputFieldSlotClick(Vector2 slotCoord)
        {
            EventInputFieldSlotClick?.Invoke(slotCoord);
        }

        public void RequestFieldInteractableChanged()
        {
            EventInteractableChanged?.Invoke();
        }

        public void RequestUpdateInteractableView()
        {
            EventUpdateInteractableView?.Invoke();
        }
    }
}