using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class GameWinPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        public Button btnBack;
        public Button btnChoose;
        public GameObject choosePanel;
            
        public TurnBaseManager turnBaseManager;

        private void Reset()
        {
            turnBaseManager = GameObject.Find("TurnManage").GetComponent<TurnBaseManager>();
        }

        private void Awake()
        {
            btnBack.onClick.AddListener(
                ()=>
                {
                    turnBaseManager.ExitTurn();
                    gameObject.SetActive(false);//关闭自己
                });
            btnChoose.onClick.AddListener(() =>
                {
                    choosePanel.SetActive(true);
                }
                );
        }
    }
}