using System;

namespace Game.Core
{
    public interface IGameEventsModel
    {
        public event Action EventWin;
        public event Action EventRestart;
    }
}