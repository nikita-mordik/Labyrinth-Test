using FreedLOW._Maze.Scripts.Extension;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.UI.Panels
{
    public class MenuPanelMediator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup menuPanel;
        [SerializeField] private CanvasGroup levelPanel;
        [SerializeField] private CanvasGroup shopPanel;

        public void OpenMenuPanel()
        {
            menuPanel.State(true);
            levelPanel.State(false);
            shopPanel.State(false);
        }

        public void OpenLevelPanel()
        {
            menuPanel.State(false);
            levelPanel.State(true);
            shopPanel.State(false);
        }

        public void OpenShopPanel()
        {
            menuPanel.State(false);
            levelPanel.State(false);
            shopPanel.State(true);
        }
    }
}