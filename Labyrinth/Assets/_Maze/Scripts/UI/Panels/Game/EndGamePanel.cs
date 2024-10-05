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
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup endPanelGroup;
        [SerializeField] private TextMeshProUGUI finishText;
        [SerializeField] private Button restartButton;

        private bool isLoose;

        private GameStateMachine stateMachine;
        private IGameEventHandlerService gameEventHandlerService;
        private IPersistentProgressService progressService;
        private IGameFactory gameFactory;

        public void Construct(GameStateMachine stateMachine, IGameEventHandlerService gameEventHandlerService,
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
        }

        private void OnDestroy()
        {
            gameEventHandlerService.OnFinishGame -= OnFinishGame;
            gameEventHandlerService.OnLooseGame -= OnLooseGame;
        }

        private void OnFinishGame()
        {
            finishText.text = "You are escape successfully!";
            endPanelGroup.State(true);
            isLoose = false;
            stateMachine.Enter<PauseState>();
        }

        private void OnLooseGame()
        {
            finishText.text = "You are not escape!";
            endPanelGroup.State(true);
            isLoose = true;
            stateMachine.Enter<PauseState>();
        }

        private void OnRestart()
        {
            if (isLoose)
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
            else
            {
                PlayerPrefs.DeleteAll();
                stateMachine.Enter<BootstrapState>();
            }
        }
    }
}