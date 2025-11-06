using System;
using UnityEngine;

namespace Game.Core
{
    [Serializable]
    public struct LevelVariantData
    {
        [field: SerializeField, Min(1)]
        public int FieldSizeX { get; private set; }
        
        [field: SerializeField, Min(1)]
        public int FieldSizeY { get; private set; }
        
        [field: SerializeField]
        public int[] Iterations { get; private set; }
    }
}