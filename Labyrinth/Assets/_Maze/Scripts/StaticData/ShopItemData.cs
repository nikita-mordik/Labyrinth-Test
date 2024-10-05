using UnityEngine;

namespace FreedLOW._Maze.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "FreedLOW/Maze/Shop")]
    public class ShopItemData : ScriptableObject
    {
        public ItemType ItemType;
        public GameObject Prefab;
        public string ItemName;
        public Sprite ItemSprite;
        public int ItemPrice;
    }
}