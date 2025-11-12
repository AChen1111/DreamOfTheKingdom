using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : MonoBehaviour
{
    public TurnBaseManager turnBaseManager;
    private VisualElement rootElement;
    private Label energyNum,drawNum,disNum,turnLabel;
    private Button turnButton;
    
    private void OnEnable()
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
    /// 监听事件
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDrawNum(int num)
    {
        drawNum.text = num.ToString();
    }
    /// <summary>
    /// 监听事件
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDisNum(int num)
    {
        disNum.text = num.ToString();
    }

    public void UpdateTurnLabel()
    {
        switch (turnLabel.text)
        {
            case "玩家回合" :
                turnLabel.text = "敌人回合";
                break;
            case "敌人回合":
                turnLabel.text = "玩家回合";
                break;
        }
    }
}
