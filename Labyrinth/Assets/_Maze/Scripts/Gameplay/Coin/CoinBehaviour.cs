using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Gameplay.Coin
{
    public class CoinBehaviour : MonoBehaviour
    {
        [SerializeField] private int amount;
        
        private PlayerProgress _playerProgress;

        private void Start()
        {
            _playerProgress = AllServices.Container.Single<IPersistentProgressService>().PlayerProgress;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Hero))
            {
                UpdateMoney();
                Destroy(gameObject);
            }
        }

        private void UpdateMoney()
        {
            _playerProgress.WorldData.MoneyData.ChangeMoneyCount(amount);
        }
    }
}