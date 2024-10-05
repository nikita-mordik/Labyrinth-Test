using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Hero;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Game
{
    public class AbilityPanel : MonoBehaviour
    {
        [SerializeField] private Button boostButton;
        [SerializeField] private Button invisibilityButton;
        [SerializeField] private TextMeshProUGUI boostCountText;
        [SerializeField] private TextMeshProUGUI invisibilityCountText;
        
        private PlayerProgress _playerProgress;
        private HeroStats _heroStats;

        public void Construct(IPersistentProgressService progressService, HeroStats hero)
        {
            _playerProgress = progressService.PlayerProgress;
            _playerProgress.WorldData.AbilityData.BoostAbility.OnBoostCountChanged += CheckBoostAbilityCount;
            _playerProgress.WorldData.AbilityData.InvisibilityAbility.OnInvisibilityCountChanged += CheckInvisibilityAbilityCount;
            _heroStats = hero;

            CheckBoostAbilityCount(_playerProgress.WorldData.AbilityData.BoostAbility.BoostCount);
            CheckInvisibilityAbilityCount(_playerProgress.WorldData.AbilityData.InvisibilityAbility.InvisibilityCount);
        }

        private void Start()
        {
            boostButton.onClick.AddListener(OnUseBoost);
            invisibilityButton.onClick.AddListener(OnUseInvisibility);
        }

        private void OnDestroy()
        {
            _playerProgress.WorldData.AbilityData.BoostAbility.OnBoostCountChanged -= CheckBoostAbilityCount;
            _playerProgress.WorldData.AbilityData.InvisibilityAbility.OnInvisibilityCountChanged -= CheckInvisibilityAbilityCount;
        }

        private void CheckBoostAbilityCount(int value)
        {
            boostCountText.text = value.ToString();
            
            if (!_playerProgress.WorldData.AbilityData.BoostAbility.HasBoost) 
                boostButton.interactable = false;
        }

        private void CheckInvisibilityAbilityCount(int value)
        {
            invisibilityCountText.text = value.ToString();
            
            if (!_playerProgress.WorldData.AbilityData.InvisibilityAbility.HasInvisibility) 
                invisibilityButton.interactable = false;
        }

        private void OnUseBoost()
        {
            _heroStats.ApplyBoostToHero();
            _playerProgress.WorldData.AbilityData.BoostAbility.ChangeBoostCount(-1);
        }

        private void OnUseInvisibility()
        {
            _heroStats.ApplyInvisibilityToHero();
            _playerProgress.WorldData.AbilityData.InvisibilityAbility.ChangeInvisibilityCount(-1);
        }
    }
}