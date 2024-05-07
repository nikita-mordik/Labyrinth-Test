using System;

namespace Infrastructure.Services.EventHandler
{
    public class GameEventHandlerService : IGameEventHandlerService
    {
        public event Action OnFinishGame;
        public event Action OnLooseGame;
        
        public void InvokeOnFinishGame() => 
            OnFinishGame?.Invoke();

        public void InvokeOnLooseGame() => 
            OnLooseGame?.Invoke();
    }
}