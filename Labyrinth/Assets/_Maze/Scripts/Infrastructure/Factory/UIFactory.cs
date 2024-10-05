using FreedLOW._Maze.Scripts.Infrastructure.AssetManagement;
using FreedLOW._Maze.Scripts.StaticData;
using FreedLOW._Maze.Scripts.UI.Panels.Menu;
using FreedLOW._Maze.Scripts.UI.Shop;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string StaticDataPath = "StaticData";
        
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void CreateBoostShopItem(Transform at, ShopPanel shopPanel)
        {
            var itemData = _assetProvider.Load<ShopItemData>($"{StaticDataPath}/BoostShopItem");
            var gameObject = Object.Instantiate(itemData.Prefab, at);
            var shopItem = gameObject.GetComponent<ShopItem>();
            shopItem.Construct(itemData, shopPanel);
        }
        
        public void CreateInvisibilityShopItem(Transform at, ShopPanel shopPanel)
        {
            var itemData = _assetProvider.Load<ShopItemData>($"{StaticDataPath}/InvisibilityShopItem");
            var gameObject = Object.Instantiate(itemData.Prefab, at);
            var shopItem = gameObject.GetComponent<ShopItem>();
            shopItem.Construct(itemData, shopPanel);
        }
    }
}