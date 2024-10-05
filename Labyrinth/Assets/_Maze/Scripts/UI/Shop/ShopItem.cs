using FreedLOW._Maze.Scripts.StaticData;
using FreedLOW._Maze.Scripts.UI.Panels.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button itemButton;
        [SerializeField] private Outline outline;
        
        [Header("Shop data")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image itemImage;
        
        private ShopPanel _shopPanel;
        
        public ItemType ItemType { get; private set; }
        public int Price { get; private set; }

        public void Construct(ShopItemData itemData, ShopPanel shopPanel)
        {
            _shopPanel = shopPanel;

            ItemType = itemData.ItemType;
            Price = itemData.ItemPrice;
            nameText.text = itemData.ItemName;
            priceText.text = $"{Price} $";
            itemImage.sprite = itemData.ItemSprite;
            
            itemButton.onClick.AddListener(OnItemSelect);
        }

        public void Deselect()
        {
            outline.enabled = false;
        }

        private void OnItemSelect()
        {
            _shopPanel.UpdateSelected(this);
            outline.enabled = true;
        }
    }
}