using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace FreedLOW._Maze.Scripts.Infrastructure
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(sceneName))
            {
                onSceneLoaded?.Invoke();
                return;
            }
            
            var handler = SceneManager.LoadSceneAsync(sceneName);
            await handler.ToUniTask();
            
            onSceneLoaded?.Invoke();
        }
    }
}