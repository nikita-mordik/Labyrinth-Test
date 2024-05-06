using Extension;
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
        }

        private void OnSaveProgress()
        {
            
        }

        private void OnLoadProgress()
        {
            
        }

        private void OnBack()
        {
            pauseGroup.State(false);
        }
    }
}