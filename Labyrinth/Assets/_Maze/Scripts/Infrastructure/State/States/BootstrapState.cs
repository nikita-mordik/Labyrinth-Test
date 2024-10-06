using FreedLOW._Maze.Scripts.Infrastructure.AssetManagement;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Identifiers;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Input;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class BootstrapState : IState
    {
        private const string SceneName = "BootScene";
        
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices allServices;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            allServices = services;
            
            RegisterServices();
        }

        public async void Enter()
        {
            await sceneLoader.LoadSceneAsync(SceneName, onSceneLoaded: EnterLoadProgress);
        }

        public void Exit() { }

        private void EnterLoadProgress()
        {
            gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            allServices.RegisterSingle<IGameStateMachine>(gameStateMachine);
            allServices.RegisterSingle(InputService());
            allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            allServices.RegisterSingle<IGameEventHandlerService>(new GameEventHandlerService());
            allServices.RegisterSingle<IIdentifierService>(new IdentifierService());
            
            RegisterFactories();

            allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                allServices.Single<IGameFactory>(),
                allServices.Single<IPersistentProgressService>()));
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            
            return new MobileInputService();
        }

        private void RegisterFactories()
        {
            allServices.RegisterSingle<IGameFactory>(new GameFactory(
                allServices.Single<IAssetProvider>()));

            allServices.RegisterSingle<IUIFactory>(new UIFactory(allServices.Single<IAssetProvider>()));
        }
    }
}