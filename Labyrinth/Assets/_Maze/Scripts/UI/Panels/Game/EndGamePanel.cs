using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Extension;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
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

        public void Construct(IGameStateMachine stateMachine, IGameEventHandlerService gameEventHandlerService,
            IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.stateMachine = stateMachine;
            this.gameEventHandlerService = gameEventHandlerService;
            
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
            progress.WorldData.GameData.TotalSeconds = 240;
            progress.WorldData.GameData.AttemptsCount++;
            progress.WorldData.PositionOnLevel.Position = GameObject.FindGameObjectWithTag(Tags.HeroPoint).transform
                .position.AsVectorData();
                
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
            
            if (string.Equals(activeSceneName, SceneNames.LevelOne))
            {
                stateMachine.Enter<LoadLevelState, string>(SceneNames.LevelTwo);
            }
            else if (string.Equals(activeSceneName, SceneNames.LevelTwo))
            {
                stateMachine.Enter<LoadLevelState, string>(SceneNames.LevelThree);
            }
            else if (string.Equals(activeSceneName, SceneNames.LevelThree))
            {
                stateMachine.Enter<LoadMenuState>();
            }
        }
    }
}