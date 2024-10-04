using FreedLOW._Maze.Scripts.Infrastructure.State;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game game;

        private void Awake()
        {
            game = new Game();
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}