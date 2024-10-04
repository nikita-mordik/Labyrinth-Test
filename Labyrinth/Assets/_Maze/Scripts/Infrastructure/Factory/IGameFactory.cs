using System.Collections.Generic;
using FreedLOW._Maze.Scripts.Enemy;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Factory
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