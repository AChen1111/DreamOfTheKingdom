using Character;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class ResetRoomPanel : MonoBehaviour
    {

        [Header("UI Elements")]
        public Button btnBack;
        public Button btnChoose;
        public TurnBaseManager turnBaseManager;
        public Player player;
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
                    player.ResetStats();
                    gameObject.SetActive(false);
                    turnBaseManager.ExitTurn();
                }
            );
        }
    }
}
