namespace Game.Core
{
    public interface IGameEventsRequestsModel
    {
        void RequestWin();
        void RequestLose();
        void RequestRestart();
    }
}