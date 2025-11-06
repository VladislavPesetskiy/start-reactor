namespace Game.Environment.Slots.Scripts
{
    public struct FieldSlotInteractableControllerArgs
    {
        public FieldSlotView View { get; }

        public FieldSlotInteractableControllerArgs(FieldSlotView view)
        {
            View = view;
        }
    }
}