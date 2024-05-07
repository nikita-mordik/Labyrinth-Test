using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Services.EventHandler;
using Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePanel : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Button pauseButton;
        [SerializeField] private PausePanel pausePanel;

        private const int MillisecondsDelay = 1000;

        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private int attemptsCount;
        private int seconds;

        private IGameEventHandlerService gameEventHandlerService;

        public void Construct(IGameEventHandlerService gameEventHandlerService)
        {
            this.gameEventHandlerService = gameEventHandlerService;
        }

        private void Start()
        {
            pauseButton.onClick.AddListener(OnPause);
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

        private void UpdateUI(int leftSecond)
        {
            cancellationToken?.Cancel();
            cancellationToken = new CancellationTokenSource();
            
            OnChangeAttempts();
            StartTimer(leftSecond);
        }
    }
}