using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Extension;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using FreedLOW._Maze.Scripts.Infrastructure.State;
using FreedLOW._Maze.Scripts.Infrastructure.State.States;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Game
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup endPanelGroup;
        [SerializeField] private TextMeshProUGUI finishText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextLevelButton;

        private IGameStateMachine stateMachine;
        private IGameEventHandlerService gameEventHandlerService;
        private IPersistentProgressService progressService;
        private IGameFactory gameFactory;
        private ISaveLoadService _saveLoadService;

        public void Construct(IGameStateMachine stateMachine, IGameEventHandlerService gameEventHandlerService,
            IPersistentProgressService progressService, IGameFactory gameFactory, ISaveLoadService saveLoadService)
        {
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.stateMachine = stateMachine;
            this.gameEventHandlerService = gameEventHandlerService;
            _saveLoadService = saveLoadService;

            this.gameEventHandlerService.OnFinishGame += OnFinishGame;
            this.gameEventHandlerService.OnLooseGame += OnLooseGame;
        }

        private void Start()
        {
            endPanelGroup.State(false);
            restartButton.onClick.AddListener(OnRestart);
            nextLevelButton.onClick.AddListener(OnLoadNextLevel);
        }

        private void OnDestroy()
        {
            gameEventHandlerService.OnFinishGame -= OnFinishGame;
            gameEventHandlerService.OnLooseGame -= OnLooseGame;
        }

        private void OnFinishGame()
        {
            finishText.text = "You are escape successfully!";
            restartButton.gameObject.SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            endPanelGroup.State(true);
            stateMachine.Enter<PauseState>();
        }

        private void OnLooseGame()
        {
            finishText.text = "You are not escape!";
            restartButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
            endPanelGroup.State(true);
            stateMachine.Enter<PauseState>();
        }

        private void OnRestart()
        {
            var progress = progressService.PlayerProgress;
            progress.WorldData.GameData.IsRestart = true;
            progress.WorldData.GameData.TotalSeconds = 240;
            progress.WorldData.GameData.AttemptsCount++;
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                GameObject.FindGameObjectWithTag(Tags.HeroPoint).transform.position.AsVectorData());
                
            foreach (var progressReader in gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(progressService.PlayerProgress);
            }
                
            endPanelGroup.State(false);
            stateMachine.Enter<GameLoopState>();
        }

        private void OnLoadNextLevel()
        {
            var activeSceneName = SceneManager.GetActiveScene().name;
            var progress = progressService.PlayerProgress;
            progress.WorldData.EnemyData.EnemyPositionOnLevels.Clear();
            
            if (string.Equals(activeSceneName, SceneNames.LevelOne))
            {
                progress.WorldData.CompletedLevelData.IsLevelTwoOpen = true;
                stateMachine.Enter<LoadLevelState, string>(SceneNames.LevelTwo);
            }
            else if (string.Equals(activeSceneName, SceneNames.LevelTwo))
            {
                progress.WorldData.CompletedLevelData.IsLevelThreeOpen = true;
                stateMachine.Enter<LoadLevelState, string>(SceneNames.LevelThree);
            }
            else if (string.Equals(activeSceneName, SceneNames.LevelThree))
            {
                stateMachine.Enter<LoadMenuState>();
            }
        }
        
        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}