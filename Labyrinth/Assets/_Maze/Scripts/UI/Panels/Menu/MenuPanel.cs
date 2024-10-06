using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [FormerlySerializedAs("panelMediator")] [SerializeField] private MenuPanelMediator menuPanelMediator;
        [SerializeField] private GameObject webViewPrefab;

        [Header("Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button webViewButton;
        
        private UniWebView _webView;

        private void Start()
        {
            playButton.onClick.AddListener(OnOpenLevelPanel);
            shopButton.onClick.AddListener(OnOpenShopPanel);
            webViewButton.onClick.AddListener(OnOpenWebView);
            
            CreateWebView();
        }

        private void OnOpenLevelPanel()
        {
            menuPanelMediator.OpenLevelPanel();
        }

        private void OnOpenShopPanel()
        {
            menuPanelMediator.OpenShopPanel();
        }

        private void OnOpenWebView()
        {
            if (_webView == null) 
                CreateWebView();
            
            _webView.Load("https://www.google.com/");
            _webView.Show();
        }

        private void CreateWebView()
        {
            if (_webView == null) 
                _webView = Instantiate(webViewPrefab).GetComponent<UniWebView>();
            
            _webView.OnShouldClose += (view) => {
                Destroy(_webView.gameObject);
                _webView = null;
                return true;
            };
        }
    }
}