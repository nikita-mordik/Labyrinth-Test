using UnityEngine;
using UnityEngine.UI;

namespace FreedLOW._Maze.Scripts.UI.Panels.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private PanelMediator panelMediator;
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
            shopButton.onClick.AddListener(OnOpenWebView);
            
            CreateWebView();
        }

        private void OnOpenLevelPanel()
        {
            panelMediator.OpenLevelPanel();
        }

        private void OnOpenShopPanel()
        {
            panelMediator.OpenShopPanel();
        }

        private void OnOpenWebView()
        {
            if (_webView == null) 
                CreateWebView();
            
            _webView.Load("https://www.google.com/");
            _webView.Show(true, UniWebViewTransitionEdge.Top);
        }

        private void CreateWebView()
        {
            if (_webView == null) 
                _webView = Instantiate(webViewPrefab).GetComponent<UniWebView>();
            
            _webView.OnShouldClose += (view) => {
                _webView = null;
                return true;
            };
        }
    }
}