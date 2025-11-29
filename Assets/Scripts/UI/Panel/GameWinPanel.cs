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
            //todo:卡牌选择的监听
        }
    }
}