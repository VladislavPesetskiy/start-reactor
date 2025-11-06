namespace Game.Saves.Scripts
{
    public interface IPersistence
    {
        string SaveKey { get; }
        object Persistence { get; }
        void Load();
        void Save();
    }
}