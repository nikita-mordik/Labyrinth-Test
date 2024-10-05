using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.State;

namespace FreedLOW._Maze.Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutine)
        {
            StateMachine = new GameStateMachine(coroutine, new SceneLoader(), AllServices.Container);
        }
    }
}