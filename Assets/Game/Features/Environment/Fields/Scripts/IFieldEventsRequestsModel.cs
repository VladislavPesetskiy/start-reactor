using UnityEngine;

namespace Game.Environment.Fields
{
    public interface IFieldEventsRequestsModel
    {
        void RequestReferenceFieldSlotClick(Vector2 slotCoord);
        void RequestInputFieldSlotClick(Vector2 slotCoord);
        void RequestFieldInteractableChanged();
        void RequestUpdateInteractableView();
    }
}