using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressesWriter in gameFactory.ProgressesWriters)
            {
                progressesWriter.UpdateProgress(progressService.PlayerProgress);
            }
            
            PlayerPrefs.SetString(ProgressKey, progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}