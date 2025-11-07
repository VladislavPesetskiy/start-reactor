using System;

namespace Game.Core
{
    public class GameEventsModel : IGameEventsModel, IGameEventsRequestsModel
    {
        public event Action EventWin;
        public event Action EventRestart;

        public void RequestWin()
        {
            EventWin?.Invoke();
        }
        
        public void RequestRestart()
        {
            EventRestart?.Invoke();
        }
    }
}