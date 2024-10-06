using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.State;
using FreedLOW._Maze.Scripts.Infrastructure.State.States;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Menu
{
    public class LevelPanel : MonoBehaviour
    {
        [FormerlySerializedAs("panelMediator")] [SerializeField] private MenuPanelMediator menuPanelMediator;
        
        [Header("Buttons")]
        [SerializeField] private Button levelOneButton;
        [SerializeField] private Button levelTwoButton;
        [SerializeField] private Button levelThreeButton;
        [SerializeField] private Button closeButton;
        
        private IGameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();

            levelOneButton.onClick.AddListener(OnLoadLevelOne);
            levelTwoButton.onClick.AddListener(OnLoadLevelTwo);
            levelThreeButton.onClick.AddListener(OnLoadLevelThree);
            closeButton.onClick.AddListener(OnOpenMenuPanel);

            CheckLevels();
        }

        private void OnLoadLevelOne()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.LevelOne);
        }

        private void OnLoadLevelTwo()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.LevelTwo);
        }

        private void OnLoadLevelThree()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.LevelThree);
        }

        private void OnOpenMenuPanel()
        {
            menuPanelMediator.OpenMenuPanel();
        }

        private void CheckLevels()
        {
            var playerProgress = AllServices.Container.Single<IPersistentProgressService>().PlayerProgress;

            if (playerProgress.WorldData.CompletedLevelData.IsLevelTwoOpen) 
                levelTwoButton.interactable = true;
            
            if (playerProgress.WorldData.CompletedLevelData.IsLevelThreeOpen) 
                levelThreeButton.interactable = true;
        }
    }
}