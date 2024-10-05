using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class PauseState : IState
    {
        private readonly GameStateMachine gameStateMachine;

        public PauseState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}