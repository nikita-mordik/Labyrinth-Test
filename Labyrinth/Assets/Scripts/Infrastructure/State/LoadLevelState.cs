using Common;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.State
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService progressService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        public async void Enter(string sceneName)
        {
            gameFactory.CleanUp();
            await sceneLoader.LoadSceneAsync(sceneName, OnLoaded);
        }
        
        public void Exit() { }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(progressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            Vector3 at = GameObject.FindGameObjectWithTag(Tags.HeroPoint).transform.position;
            GameObject hero = InitializeHero(at);
            InitializeHUD();
            CameraFollow(hero);
        }

        private GameObject InitializeHero(Vector3 at) => 
             gameFactory.CreateHero(at);

        private void InitializeHUD()
        {
            GameObject hud = gameFactory.CreateHud();
            
        }

        private void CameraFollow(GameObject follower) => 
            Camera.main.GetComponent<CameraFollow>().Follow(follower);
    }
}