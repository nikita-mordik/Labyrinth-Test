using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Button pauseButton;
        [SerializeField] private PausePanel pausePanel;

        private const int MillisecondsDelay = 1000;
        
        private int attemptsCount;
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        
        private void Start()
        {
            pauseButton.onClick.AddListener(OnPause);
        }
        
        public async void StartTimer(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            try
            {
                for (int i = totalSeconds; i > 0; i--)
                {
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
            
            // TODO: here invoke end timer event
            
        }

        private void OnPause()
        {
            pausePanel.ShowPausePanel();
        }

        private void OnChangeAttempts()
        {
            attemptsCount++;
            attemptsText.text = $"Number of attempts: {attemptsCount}";
        }
    }
}