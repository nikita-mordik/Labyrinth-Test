using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Game
{
    public class GamePanel : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Button pauseButton;
        [SerializeField] private PausePanel pausePanel;

        private const int MillisecondsDelay = 1000;
        private const string MoneyPattern = "Money: {0} $";

        private CancellationTokenSource cancellationToken = new();
        private int attemptsCount;
        private int seconds;

        private IGameEventHandlerService gameEventHandlerService;
        private PlayerProgress _playerProgress;

        public void Construct(IGameEventHandlerService gameEventHandlerService, IPersistentProgressService progressService)
        {
            this.gameEventHandlerService = gameEventHandlerService;
            _playerProgress = progressService.PlayerProgress;
            _playerProgress.WorldData.MoneyData.OnMoneyAmountChanged += MoneyAmountChanged;
        }

        private void Start()
        {
            pauseButton.onClick.AddListener(OnPause);
        }

        private void OnDestroy()
        {
            _playerProgress.WorldData.MoneyData.OnMoneyAmountChanged -= MoneyAmountChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            attemptsCount = progress.WorldData.GameData.AttemptsCount;
            seconds = progress.WorldData.GameData.TotalSeconds;

            UpdateUI(seconds);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.GameData.AttemptsCount = attemptsCount;
            progress.WorldData.GameData.TotalSeconds = seconds;
        }

        private void MoneyAmountChanged(int moneyCount)
        {
            moneyText.text = string.Format(MoneyPattern, moneyCount);
        }

        private async void StartTimer(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            try
            {
                for (int i = totalSeconds; i >= 0; i--)
                {
                    seconds = i;
                    minutes = i / 60;
                    remainingSeconds = i % 60;

                    timeText.text = $"Left time: {minutes:D2}:{remainingSeconds:D2}";

                    await UniTask.Delay(MillisecondsDelay, false, PlayerLoopTiming.Update, cancellationToken.Token);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                return;
            }
            
            gameEventHandlerService.InvokeOnLooseGame();
        }

        private void OnPause()
        {
            pausePanel.ShowPausePanel();
        }

        private void OnChangeAttempts() => 
            attemptsText.text = $"Number of attempts: {attemptsCount}";

        private void UpdateUI(int leftSecond)
        {
            cancellationToken?.Cancel();
            cancellationToken = new CancellationTokenSource();
            
            OnChangeAttempts();
            StartTimer(leftSecond);
        }
    }
}