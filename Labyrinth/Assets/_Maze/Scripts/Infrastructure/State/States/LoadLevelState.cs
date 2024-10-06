﻿using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Enemy;
using FreedLOW._Maze.Scripts.Hero;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Identifiers;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Input;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using FreedLOW._Maze.Scripts.UI.Panels.Game;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.State.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IGameEventHandlerService gameEventHandlerService;
        private readonly IInputService inputService;
        private readonly ISaveLoadService saveLoadService;
        private readonly IIdentifierService identifierService;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private HeroStats _heroStats;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService progressService, IGameEventHandlerService gameEventHandlerService, 
            IInputService inputService, ISaveLoadService saveLoadService, IIdentifierService identifierService,
            ICoroutineRunner coroutineRunner)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.gameEventHandlerService = gameEventHandlerService;
            this.inputService = inputService;
            this.saveLoadService = saveLoadService;
            this.identifierService = identifierService;
            _coroutineRunner = coroutineRunner;
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
            var point = GameObject.FindGameObjectWithTag(Tags.HeroPoint).transform;
            GameObject hero = InitializeHero(at: point.position, point.rotation);
            InitializeHUD();
            CameraFollow(hero);
            InitializeEnemies(hero);
        }

        private GameObject InitializeHero(Vector3 at, Quaternion pointRotation)
        {
            _heroStats = new HeroStats(3, _coroutineRunner);
            
            var hero = gameFactory.CreateHero(at);
            hero.transform.rotation = pointRotation;
            hero.GetComponent<HeroMove>()
                .Construct(inputService, _heroStats);
            hero.GetComponentInChildren<HeroDeath>()
                .Construct(gameEventHandlerService);
            return hero;
        }

        private void InitializeHUD()
        {
            GameObject hud = gameFactory.CreateHud();
            hud.GetComponentInChildren<GamePanel>()
                .Construct(gameEventHandlerService, progressService);
            hud.GetComponentInChildren<AbilityPanel>()
                .Construct(progressService, _heroStats);
            hud.GetComponentInChildren<PausePanel>()
                .Construct(gameStateMachine, saveLoadService, gameFactory, progressService);
            hud.GetComponentInChildren<EndGamePanel>()
                .Construct(gameStateMachine, gameEventHandlerService, progressService, gameFactory, saveLoadService);
        }

        private void CameraFollow(GameObject follower) => 
            Camera.main.GetComponent<CameraFollow>().Follow(follower);

        private void InitializeEnemies(GameObject hero)
        {
            GameObject[] immovablePoints = GameObject.FindGameObjectsWithTag(Tags.ImmovablePoint);
            GameObject[] walkablePoints = GameObject.FindGameObjectsWithTag(Tags.WalkablePoint);

            for (int i = 0; i < immovablePoints.Length; i++)
            {
                var point = immovablePoints[i].transform;
                var enemy = gameFactory.CreateEnemy(PatrollingType.Immovable);
                enemy.GetComponent<BaseAIEnemy>().Construct(identifierService);
                enemy.GetComponent<EnemyImmovableAIController>()
                    .Initialize(point, hero);
            }

            for (int i = 0; i < walkablePoints.Length; i++)
            {
                var point = walkablePoints[i].transform;
                var enemy = gameFactory.CreateEnemy(PatrollingType.Walkable);
                enemy.GetComponent<BaseAIEnemy>().Construct(identifierService);
                enemy.GetComponent<EnemyWalkableAIController>()
                    .Initialize(point, hero);
            }
        }
    }
}