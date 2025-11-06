using UnityEngine;

namespace Game.Environment.Fields
{
    public class ReferenceFieldView : FieldView
    {
        [field: SerializeField]
        public SpriteRenderer Background { get; private set; }
    }
}