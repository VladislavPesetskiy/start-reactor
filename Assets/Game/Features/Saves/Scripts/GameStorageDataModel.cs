namespace Game.Saves.Scripts
{
    public class GameStorageDataModel : DataModel<GameStorageData>
    {
        public int CurrentLevelIndex => Data.CurrentLevelIndex;

        public void IncreaseLevelIndex()
        {
            Data.CurrentLevelIndex++;
        }
    }
}