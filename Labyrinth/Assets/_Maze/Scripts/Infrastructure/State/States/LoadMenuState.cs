using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class LoadMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public async void Enter()
        {
            _gameFactory.CleanUp();
            await _sceneLoader.LoadSceneAsync(SceneNames.Menu);
        }

        public void Exit() { }
    }
}