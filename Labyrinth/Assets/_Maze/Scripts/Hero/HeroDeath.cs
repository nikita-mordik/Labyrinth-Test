using FreedLOW._Maze.Scripts.Enemy;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        private IGameEventHandlerService gameEventHandlerService;

        public void Construct(IGameEventHandlerService gameEventHandlerService)
        {
            this.gameEventHandlerService = gameEventHandlerService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BaseAIEnemy>(out var enemy)) 
                gameEventHandlerService.InvokeOnLooseGame();
        }
    }
}