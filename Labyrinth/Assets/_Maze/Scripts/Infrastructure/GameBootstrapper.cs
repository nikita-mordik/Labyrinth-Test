using FreedLOW._Maze.Scripts.Infrastructure.State.States;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game game;

        private void Awake()
        {
            game = new Game(this);
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}