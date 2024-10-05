using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.UI.Panels.Menu;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        void CreateBoostShopItem(Transform at, ShopPanel shopPanel);
        void CreateInvisibilityShopItem(Transform at, ShopPanel shopPanel);
    }
}