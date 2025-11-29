using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Panel
{
    public class GameLosePanel : MonoBehaviour
    {
        public Button btnBack;
        public TurnBaseManager turnBaseManager;

        private void Reset()
        {
            turnBaseManager = GameObject.Find("TurnManage").GetComponent<TurnBaseManager>();
            btnBack = GetComponentInChildren<Button>();
        }

        private void Awake()
        {
            btnBack = GetComponentInChildren<Button>();
            //todo:回到主界面
        }
    }
}