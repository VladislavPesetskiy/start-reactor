using Game.Core;

namespace Game.Environment.Fields
{
    public struct FieldControllerArgs
    {
        public LevelVariantData LevelVariantData { get; }

        public FieldControllerArgs(LevelVariantData levelVariantData)
        {
            LevelVariantData = levelVariantData;
        }
    }
}