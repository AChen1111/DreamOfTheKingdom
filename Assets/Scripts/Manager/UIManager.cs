using System;
using Character;
using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public GameObject currentPanel;
        
        [Header("Panel")]
        public GameObject menuPanel;
        public GameObject playerPanel;
        public GameObject player;
        public GameObject gameOverPanel;
        public GameObject gameWinPanel;
        
        public static UIManager Instance{get; private set;}

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
        
        /// <summary>
        /// 卸载 画布
        /// </summary>
        private void unloadPanel()
        {
            if(currentPanel != null)
                currentPanel.SetActive(false);
            currentPanel = null;
        }
        
        /// <summary>
        /// 加载 指定画布
        /// </summary>
        /// <param name="panel"></param>
        private void loadPanel(GameObject panel)
        {
            currentPanel = panel;
            currentPanel.SetActive(true);
        }
        
        /// <summary>
        /// 进入房间时 调用
        /// </summary>
        public void OnRoomLoaded()
        {
            player.SetActive(true);
            loadPanel(playerPanel);
        }
        
        /// <summary>
        /// 离开房间时 调用
        /// </summary>
        public void OnRoomOver()
        {
            player.SetActive(false);
            unloadPanel();
        }
        
        /// <summary>
        /// 进入菜单界面
        /// </summary>
        public void OnMenuLoaded()
        {
            unloadPanel();
            loadPanel(menuPanel);
            player.SetActive(false);
        }
        
        /// <summary>
        /// 卸载 菜单界面
        /// </summary>
        public void OnMenuOver()
        {
            unloadPanel();
        }
        
        public void OnGameWin()
        {
            gameWinPanel.SetActive(true);
        }

        public void OnGameOver()
        {
            gameOverPanel.SetActive(true);
        }
    }
}