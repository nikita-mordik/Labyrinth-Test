﻿using System.Collections.Generic;
using Enemy;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IPersistentProgressService progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressesWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IPersistentProgressService progressService)
        {
            this.assetProvider = assetProvider;
            this.progressService = progressService;
        }

        public GameObject CreateHero(Vector3 at)
        {
            HeroGameObject = InstantiateRegister(prefabPath: AssetsPath.Hero, at);
            return HeroGameObject;
        }

        public GameObject CreateHud() => InstantiateRegister(AssetsPath.HUD);

        public GameObject CreateEnemy(PatrollingType type)
        {
            return type switch
            {
                PatrollingType.Walkable => InstantiateRegister(AssetsPath.WalkableEnemy),
                PatrollingType.Immovable => InstantiateRegister(AssetsPath.ImmovableEnemy),
                _ => null
            };
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressesWriters.Clear();
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                RegisterProgressReader(progressReader);
            }
        }

        private void RegisterProgressReader(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressesWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegister(string prefabPath, Vector3 at)
        {
            GameObject gameObject = assetProvider.Instantiate(prefabPath, at: at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegister(string prefabPath)
        {
            GameObject gameObject = assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
    }
}