namespace Game.Environment.Slots.Scripts
{
    public class FieldSlotData
    {
        public int CoordX { get; }
        public int CoordY { get; }

        public FieldSlotData(int coordX, int coordY)
        {
            CoordX = coordX;
            CoordY = coordY;
        }
    }
}