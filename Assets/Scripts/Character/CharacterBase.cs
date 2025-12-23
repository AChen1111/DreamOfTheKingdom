using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class CharacterBase : MonoBehaviour {
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        public bool isDead = false;
        public int maxHp;
        public IntVariable hp;
        public IntVariable defense;
        public IntVariable buffRound;//buff的持续回合数
    
        public GameObject buff;
        public GameObject deBuff;
    
        //攻击力基数
        private float atkBase = 1.0f;
        private float preiAtkBase; //初始基数 只在Start赋值一次
        public float AtkBase
        {
            get => atkBase;
            private  set => atkBase = value;
        }

        //攻击增益效果
        private float atkBuff = 0.5f;
        //攻击减益效果
        private float atkDeBuff = 0.25f;
    
        public int CurrentHP
        {
            get { return hp.currentValue; }
            set { hp.SetValue(value); }
        }
    
        public int MaxHp
        {
            get { return maxHp; }
        }
    
        protected Animator animator;
    
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }
    
        protected virtual void Start()
        {
            hp.maxValue = maxHp;
            CurrentHP = MaxHp;
            buffRound.SetValue(0);
            preiAtkBase = atkBase;
            ResetDefense();
        }
        public virtual void TakeDamage(int damage)
        {
            Debug.Log($"[TakeDamage] {name} damage={damage} hp={CurrentHP} def={defense.currentValue}\n{Environment.StackTrace}");
            if (isDead) return;
            
            //伤害够
            if (damage >= defense.currentValue)
            {
                CurrentHP -= (damage  - defense.currentValue);
                defense.SetValue(0);
                animator.SetTrigger(Hit);
            }
            //伤害不够
            else
            {
                defense.SetValue
                    (defense.currentValue - damage);
            }

            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                isDead = true;
                animator.SetBool(IsDead, true);
            }
        }
    
        /// <summary>
        /// 更新防御值(外部调用)
        /// </summary>
        /// <param name="value"></param>
        public void UpdateDefense(int value)
        {
            var cur = defense.currentValue + value;
            defense.SetValue(cur);
        }
    
        /// <summary>
        /// 重置防御值 (玩家脚本监听 玩家回合开始)
        /// </summary>
        public void ResetDefense()
        {
            defense.SetValue(0);
        }
    
        /// <summary>
        /// 回血 对外接口
        /// </summary>
        /// <param name="value"></param>
        public void HealHealth(int value)
        {
            Debug.LogWarning($"[HealHealth] {name} value={value} hp={CurrentHP}\n{Environment.StackTrace}");
            CurrentHP += value;
            CurrentHP = Mathf.Min(CurrentHP,maxHp);
            StartCoroutine(doBuffAnimation());
        }

        /// <summary>
        /// 对外接口 获得AtkBuff
        /// </summary>
        /// <param name="round"></param>
        /// <param name="isbuff">是否为增益效果</param>
        public void UpdateAtkBuffRound(int round,bool isbuff)
        {
            if (isbuff)
            { 
                atkBase = preiAtkBase + atkBuff; 
                StartCoroutine(doBuffAnimation()); 
            }
            else
            {
                atkBase = preiAtkBase - atkDeBuff;
                StartCoroutine(doDeBuffAnimation()); 
            }

            if (atkBase.Equals(1.0f))
            {
                buffRound.SetValue(0);
            }
            else
            {
                buffRound.SetValue(round + buffRound.currentValue);
            }
        }
    
        /// <summary>
        /// 监听 回合开始事件
        /// </summary>
        public void UpdateBuffRound()
        {
            if (buffRound.currentValue <= 0)
            {
                buffRound.SetValue(0);
                atkBase = preiAtkBase;//复原伤害
                return;
            }

            buffRound.SetValue(buffRound.currentValue - 1);
        }
    
        /// <summary>
        /// 播放buff动画
        /// </summary>
        /// <returns></returns>
        IEnumerator doBuffAnimation()
        {
            buff.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            buff.SetActive(false);
        }
    
        /// <summary>
        /// 播放debuff动画
        /// </summary>
        /// <returns></returns>
        IEnumerator doDeBuffAnimation()
        {
            deBuff.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            deBuff.SetActive(false);
        }
    
        /// <summary>
        /// 获取当前角色的状态
        /// </summary>
        /// <returns>
        ///  0 代表普通状态
        /// 1代表 buff状态 
        /// -1 代表debuff状态
        /// </returns>
        public int getBuffState()
        {
            if (Mathf.Approximately(atkBase, preiAtkBase)) return 0;
            else if (atkBase > preiAtkBase) return 1;
            else return -1;
        }
    }
}