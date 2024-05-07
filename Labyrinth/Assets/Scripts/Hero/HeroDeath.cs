using Enemy;
using Infrastructure.Services.EventHandler;
using UnityEngine;

namespace Hero
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