using FreedLOW._Maze.Scripts.Extension;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using FreedLOW._Maze.Scripts.Infrastructure.State;
using FreedLOW._Maze.Scripts.Infrastructure.State.States;
using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Game
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pauseGroup;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button backButton;
        
        private IGameStateMachine gameStateMachine;
        private ISaveLoadService saveLoadService;
        private IGameFactory gameFactory;
        private IPersistentProgressService progressService;

        public void Construct(IGameStateMachine gameStateMachine, ISaveLoadService saveLoadService,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.gameStateMachine = gameStateMachine;
            this.saveLoadService = saveLoadService;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        private void Start()
        {
            pauseGroup.State(false);
            
            saveButton.onClick.AddListener(OnSaveProgress);
            loadButton.onClick.AddListener(OnLoadProgress);
            menuButton.onClick.AddListener(OnLoadMenu);
            backButton.onClick.AddListener(OnBack);
        }

        private void OnLoadMenu()
        {
            gameStateMachine.Enter<LoadMenuState>();
        }

        public void ShowPausePanel()
        {
            pauseGroup.State(true);
            gameStateMachine.Enter<PauseState>();
        }

        private void HidePanel()
        {
            pauseGroup.State(false);
            gameStateMachine.Enter<GameLoopState>();
        }

        private void OnSaveProgress()
        {
            HidePanel();
            saveLoadService.SaveProgress();
        }

        private void OnLoadProgress()
        {
            if (!PlayerPrefs.HasKey("Progress")) 
                return;
            
            foreach (var progressReader in gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(progressService.PlayerProgress);
            }

            HidePanel();
        }

        private void OnBack() => HidePanel();
    }
}