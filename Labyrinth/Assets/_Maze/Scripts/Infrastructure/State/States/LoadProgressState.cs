using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IPersistentProgressService persistentProgressService;
        private readonly ISaveLoadService saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService, 
            ISaveLoadService saveLoadService)
        {
            this.gameStateMachine = gameStateMachine;
            this.persistentProgressService = persistentProgressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            gameStateMachine.Enter<LoadMenuState>();
        }

        public void Exit() { }

        private void LoadProgressOrInitNew()
        {
            persistentProgressService.PlayerProgress = saveLoadService.LoadProgress() ?? DefaultProgress();
        }

        private PlayerProgress DefaultProgress()
        {
            var progress = new PlayerProgress(initialLevel: SceneNames.LevelOne);
            progress.WorldData.GameData.TotalSeconds = 240;
            return progress;
        }
    }
}