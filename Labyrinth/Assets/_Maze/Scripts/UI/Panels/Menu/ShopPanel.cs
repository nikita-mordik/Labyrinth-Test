using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using FreedLOW._Maze.Scripts.StaticData;
using FreedLOW._Maze.Scripts.UI.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Menu
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private PanelMediator panelMediator;

        [Header("Buttons")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button buyButton;

        [Header("Shop data")]
        [SerializeField] private RectTransform container;
        [SerializeField] private TextMeshProUGUI moneyText;

        private ISaveLoadService _saveLoadService;
        private PlayerProgress _playerProgress;
        private ShopItem _currentSelected;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _playerProgress = AllServices.Container.Single<IPersistentProgressService>().PlayerProgress;
            _playerProgress.WorldData.MoneyData.OnMoneyAmountChanged += MoneyAmountChanged;

            FillShopData();
            UpdateMoneyData();
            
            closeButton.onClick.AddListener(OnOpenMenuPanel);
            buyButton.onClick.AddListener(OnBuyShopItem);
        }

        private void OnDestroy()
        {
            _playerProgress.WorldData.MoneyData.OnMoneyAmountChanged -= MoneyAmountChanged;
        }

        private void MoneyAmountChanged()
        {
            UpdateMoneyData();
        }

        private void FillShopData()
        {
            var uiFactory = AllServices.Container.Single<IUIFactory>();
            uiFactory.CreateBoostShopItem(container, this);
            uiFactory.CreateInvisibilityShopItem(container, this);
        }

        private void UpdateMoneyData()
        {
            moneyText.text = $"{_playerProgress.WorldData.MoneyData.Money.ToString()} $";
        }

        private void OnOpenMenuPanel()
        {
            panelMediator.OpenMenuPanel();
        }

        private void OnBuyShopItem()
        {
            if (_currentSelected is null)
                return;
            
            if (_currentSelected.Price > _playerProgress.WorldData.MoneyData.Money)
                return;
            
            switch (_currentSelected.ItemType)
            {
                case ItemType.Boost:
                    _playerProgress.WorldData.AbilityData.BoostAbility.ChangeBoostCount(1);
                    break;
                case ItemType.Invisibility:
                    _playerProgress.WorldData.AbilityData.InvisibilityAbility.ChangeInvisibilityCount(1);
                    break;
            }
            
            _playerProgress.WorldData.MoneyData.ChangeMoneyCount(-_currentSelected.Price);
            _saveLoadService.SaveProgress();
        }

        public void UpdateSelected(ShopItem shopItem)
        {
            _currentSelected?.Deselect();
            _currentSelected = shopItem;
        }
    }
}