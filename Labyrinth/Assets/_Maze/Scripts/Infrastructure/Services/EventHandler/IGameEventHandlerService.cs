using System;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler
{
    public interface IGameEventHandlerService : IService
    {
        event Action OnFinishGame;
        event Action OnLooseGame;

        void InvokeOnFinishGame();
        void InvokeOnLooseGame();
    }
}