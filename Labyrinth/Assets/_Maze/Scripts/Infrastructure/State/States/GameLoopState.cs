using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class GameLoopState : IState
    {
        private readonly IPersistentProgressService _progressService;

        public GameLoopState(GameStateMachine gameStateMachine, IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        public void Enter()
        {
            _progressService.PlayerProgress.WorldData.GameData.IsRestart = false;
        }

        public void Exit()
        {
            
        }
    }
}