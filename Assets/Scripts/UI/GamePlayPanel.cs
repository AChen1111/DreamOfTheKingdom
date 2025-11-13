using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : MonoBehaviour
{
    public TurnBaseManager turnBaseManager;
    private VisualElement rootElement;
    private Label energyNum,drawNum,disNum,turnLabel;
    private Button turnButton;
    
    private void Awake()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        energyNum = rootElement.Q<Label>("energynum");
        drawNum = rootElement.Q<Label>("drawnum");
        disNum = rootElement.Q<Label>("disnum");
        turnLabel = rootElement.Q<Label>("topLabel");
        turnButton = rootElement.Q<Button>("turn");
        turnButton.clickable.clicked += () =>
        {
            turnBaseManager.swapTurn();
            // UpdateTurnLabel();
        };
        
        turnLabel.text = "游戏开始";
        energyNum.text = "0";
        drawNum.text = "0";
        disNum.text = "0";
    }
    
    
    /// <summary>
    /// 监听 抽牌事件
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDrawNum(int num)
    {
        drawNum.text = num.ToString();
    }
    /// <summary>
    /// 监听 弃牌事件
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDisNum(int num)
    {
        disNum.text = num.ToString();
    }
    
    /// <summary>
    /// 监听 敌人回合开始事件
    /// </summary>
    public void OnEnemyTurnBegin()
    {
        turnButton.SetEnabled(false);
        turnLabel.text = "敌人回合";
        turnLabel.style.color = Color.red;
    }
    
    /// <summary>
    /// 监听 玩家回合开始事件
    /// </summary>
    public void OnPlayerTurnBegin()
    {
        turnButton.SetEnabled(true);
        turnLabel.text = "玩家回合";
        turnLabel.style.color = Color.white;
    }
    
    /// <summary>
    /// 监听 能量改变事件
    /// </summary>
    /// <param name="mana"></param>
    public void UpdatePlayerMana(int mana)
    {
        // if (energyNum == null)
        // {
        //     Debug.Log("当前能量槽为空");
        //     return;
        // }
        energyNum.text = mana.ToString();
    }
}
