using UnityEngine;

namespace FreedLOW._Maze.Scripts.Extension
{
    public static class UITool
    {
        /// <summary>
        /// Changing CanvasGroup state between visible and not visible
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="isVisible"></param>
        public static void State(this CanvasGroup canvas, bool isVisible)
        {
            canvas.alpha = isVisible ? 1 : 0;
            canvas.interactable = isVisible;
            canvas.blocksRaycasts = isVisible;
        }
    }
}