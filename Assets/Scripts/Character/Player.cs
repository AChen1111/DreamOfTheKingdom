using System.Collections;
using Events.ScripctsObject;
using UnityEngine;

namespace Character
{
    public class Player : CharacterBase
    {
        public IntVariable playerMana;
        public int maxMana;
        public int CurrentMana
        {
            get => playerMana.currentValue;
            set => playerMana.SetValue(value);
        }

        [Header("事件广播")] public VoidEventSO loseEvent;
        private void OnEnable()
        {
            playerMana.maxValue  = maxMana;
            StartCoroutine(WaitForUiLoad());
        
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (isDead)
            {
                loseEvent.RaiseEvent();
            }
        }

        /// <summary>
        /// 监听 回合开始事件
        /// </summary>
        public void OnPlayerTurnBegin()
        {
            CurrentMana = maxMana;
            ResetDefense();
            UpdateBuffRound();
        }

        /// <summary>
        /// 监听 卡牌打出事件
        /// </summary>
        /// <param name="cost"></param>
        public void OnCardCostHappen(int cost)
        {
            UpdateMana(cost);
        }

        private void UpdateMana(int cost)
        {
            CurrentMana -= cost;
            if (CurrentMana < 0)
            {
                CurrentMana = 0;
            }
        }

        IEnumerator WaitForUiLoad()
        {
            yield return new WaitForEndOfFrame();
            CurrentMana = playerMana.maxValue;
        }
        
        /// <summary>
        /// 增加能量
        /// </summary>
        /// <param name="value"></param>
        public void AddMana(int value)
        {
            CurrentMana += value;
        }
    }
}