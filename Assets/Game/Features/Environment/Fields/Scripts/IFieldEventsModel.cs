using System;
using UnityEngine;

namespace Game.Environment.Fields
{
    public interface IFieldEventsModel
    {
        event Action<Vector2> EventInputFieldSlotClick;
        event Action<Vector2> EventReferenceFieldSlotSelection;
        event Action EventInteractableChanged;
        event Action EventUpdateInteractableView;
    }
}