using System;
using System.Collections;
using Card.Mono;
using Character;
using Character.Enemy;
using Events.ScripctsObject;
using UnityEngine;

namespace Manager
{
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
        
        [Header("牌堆")]
        public CardDeck  cardDeck;
        
        private void Reset()
        {
            cardDeck = GameObject.Find("Card Deck").GetComponent<CardDeck>();
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
            if (turnState == state.BattleEnd) return;
            playerTurnEvent.RaiseEvent(null,this);
        }

        public void PlayerTurnEnd()
        {
            playerTurnEndEvent.RaiseEvent(null,this);
        }

        public void EnemyTurnBegin()
        {
            if (turnState == state.BattleEnd) return;
            enemyTurnEvent.RaiseEvent(null,this);
        }
        
        /// <summary>
        /// 监听 敌人回合结束事件
        /// </summary>
        public void EnemyTurnEnd()
        {
            enemyTurnEndEvent.RaiseEvent(null,this);
            swapTurn();
        }
        
    
        /// <summary>
        /// 进入回合倒计时
        /// </summary>
        /// <returns></returns>
        IEnumerator gameBeginTimer()
        {
            yield return new WaitForSeconds(0.2f);
            swapTurn();
        }
        
        /// <summary>
        /// 监听 游戏开始事件
        /// </summary>
        public void OnGameBegin()
        {
            turnState = state.None;
            //初始化牌堆
            cardDeck.InitDeck();
            StartCoroutine(gameBeginTimer());
            UIManager.Instance.OnRoomLoaded();
            EnemyManager.Instance.ResetEnemies();
        }
        
        
        
        /// <summary>
        /// 监听 游戏胜利结束事件
        /// </summary>
        public void OnGameWin()
        {
            this.turnState = state.BattleEnd;
            UIManager.Instance.OnGameWin();
        }
        /// <summary>
        /// 监听 游戏失败事件
        /// </summary>
        public void OnGameLose()
        {
            this.turnState = state.BattleEnd;
            cardDeck.DisAllCards();
            UIManager.Instance.OnGameOver();
        }
        
        /// <summary>
        /// 对外接口 退出房间时调用
        /// </summary>
        public void ExitTurn()
        {
            cardDeck.DisAllCards();
            turnState = state.BattleEnd;
            UIManager.Instance.OnRoomOver();
        }
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum state
    {
        None,
        PlayerTurn,
        EnemyTurn,
        BattleEnd
    }
}