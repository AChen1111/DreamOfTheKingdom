using Events.ScripctsObject;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class WinBossPanel : MonoBehaviour
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
            btnBack.onClick.AddListener((() =>
                    {
                        Application.Quit();
                    }
                ));
        }
        
    }
}

