using Infrastructure.Services;
using Infrastructure.State;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine(new SceneLoader(), AllServices.Container);
        }
    }
}