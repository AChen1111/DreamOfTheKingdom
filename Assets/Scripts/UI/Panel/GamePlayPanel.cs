using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class GamePlayPanel : MonoBehaviour
    {
        [Header("回合管理")]
        public TurnBaseManager turnBaseManager;

        [Header("文字显示")]
        [SerializeField] private TextMeshProUGUI energyNumText;   // 能量
        [SerializeField] private TextMeshProUGUI drawNumText;     // 抽牌数
        [SerializeField] private TextMeshProUGUI disNumText;      // 弃牌数
        [SerializeField] private TextMeshProUGUI turnLabelText;   // 顶部“玩家回合 / 敌人回合”

        [Header("按钮")]
        [SerializeField] private Button turnButton;    // 回合结束按钮

        private void Awake()
        {
            if (turnButton != null)
                turnButton.onClick.AddListener(OnTurnButtonClicked);

            // 初始化文本
            if (turnLabelText != null)
                turnLabelText.text = "游戏开始";

            UpdatePlayerMana(0);
            UpdateDrawNum(0);
            UpdateDisNum(0);
        }

        private void OnDestroy()
        {
            if (turnButton != null)
                turnButton.onClick.RemoveListener(OnTurnButtonClicked);
        }

        private void OnTurnButtonClicked()
        {
            if (turnBaseManager != null)
                turnBaseManager.swapTurn();
        }

        /// <summary>监听 抽牌事件</summary>
        public void UpdateDrawNum(int num)
        {
            if (drawNumText != null)
                drawNumText.text = num.ToString();
        }

        /// <summary>监听 弃牌事件</summary>
        public void UpdateDisNum(int num)
        {
            if (disNumText != null)
                disNumText.text = num.ToString();
        }

        /// <summary>监听 敌人回合开始事件</summary>
        public void OnEnemyTurnBegin()
        {
            if (turnButton != null)
                turnButton.interactable = false;

            if (turnLabelText != null)
            {
                turnLabelText.text = "敌人回合";
                turnLabelText.color = Color.red;
            }
        }

        /// <summary>监听 玩家回合开始事件</summary>
        public void OnPlayerTurnBegin()
        {
            if (turnButton != null)
                turnButton.interactable = true;

            if (turnLabelText != null)
            {
                turnLabelText.text = "玩家回合";
                turnLabelText.color = Color.white;
            }
        }

        /// <summary>监听 能量改变事件</summary>
        public void UpdatePlayerMana(int mana)
        {
            if (energyNumText != null)
                energyNumText.text = mana.ToString();
        }
    }
}
