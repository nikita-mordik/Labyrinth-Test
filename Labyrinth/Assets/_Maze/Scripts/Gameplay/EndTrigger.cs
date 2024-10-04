using FreedLOW._Maze.Scripts.Hero;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Gameplay
{
    public class EndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<HeroMove>(out var hero))
            {
                var gameEventHandlerService = AllServices.Container.Single<IGameEventHandlerService>();
                gameEventHandlerService.InvokeOnFinishGame();
            }
        }
    }
}