using System;
using System.Collections;
using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    [SerializeField]
    public state turnState;
    
    [Header("敌人回合持续时间")]
    public float enemyTurnDuration;
    
    [Header("事件广播")]
    public ObjectEventSO playerTurnEvent;
    public ObjectEventSO enemyTurnEvent;
    public ObjectEventSO playerTurnEndEvent;
    public ObjectEventSO enemyTurnEndEvent;

    [Header("面板")]
    public GamePlayPanel panel;
    private void Start()
    {
        turnState = state.None;
        StartCoroutine(gameBeginTimer());
    }
    
    /// <summary>
    /// 回合转换 并执行对应方法
    /// </summary>
    public void swapTurn()
    {
        if (turnState == state.PlayerTurn)
        {
            PlayerTurnEnd();
            turnState = state.EnemyTurn;
            EnemyTurnBegin();
        }
        else if(turnState is state.None or state.EnemyTurn)
        {
            turnState = state.PlayerTurn;
            PlayerTurnBegin();
        }
    }
    
    public void PlayerTurnBegin()
    {
        playerTurnEvent.RaiseEvent(null,this);
    }

    public void PlayerTurnEnd()
    {
        playerTurnEndEvent.RaiseEvent(null,this);
    }

    public void EnemyTurnBegin()
    {
        enemyTurnEvent.RaiseEvent(null,this);
        StartCoroutine(EnemyTurnTimer());
    }

    public void EnemyTurnEnd()
    {
        enemyTurnEndEvent.RaiseEvent(null,this);
        swapTurn();
    }
    
    /// <summary>
    /// 敌人回合计时器
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyTurnTimer()
    {
        yield return new WaitForSeconds(enemyTurnDuration);
        EnemyTurnEnd();
    }

    IEnumerator gameBeginTimer()
    {
        yield return new WaitForSeconds(0.2f);
        swapTurn();
    }
}

public enum state
{
    None,
    PlayerTurn,
    EnemyTurn,
    BattleEnd,
}
