using FreedLOW._Maze.Scripts.Common;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class LoadMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadSceneAsync(SceneNames.Menu);
        }

        public void Exit()
        {
            
        }
    }
}