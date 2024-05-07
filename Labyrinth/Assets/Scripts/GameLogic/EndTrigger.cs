using Hero;
using Infrastructure.Services;
using Infrastructure.Services.EventHandler;
using UnityEngine;

namespace GameLogic
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