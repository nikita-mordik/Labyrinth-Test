using Extension;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.State;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pauseGroup;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button backButton;
        
        private GameStateMachine gameStateMachine;
        private ISaveLoadService saveLoadService;
        private IGameFactory gameFactory;
        private IPersistentProgressService progressService;

        public void Construct(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService,
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
            backButton.onClick.AddListener(OnBack);
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