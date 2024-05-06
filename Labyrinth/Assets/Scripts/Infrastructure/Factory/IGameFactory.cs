using System.Collections.Generic;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
        GameObject HeroGameObject { get; }

        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        GameObject CreateEnemy(PatrollingType type);
        void CleanUp();
    }
}