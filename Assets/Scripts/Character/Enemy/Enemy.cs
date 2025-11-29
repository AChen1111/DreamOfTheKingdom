using System;
using System.Collections;
using Card_Effect;
using Tools;
using UnityEngine;

namespace Character.Enemy
{
    public class Enemy : CharacterBase
    {
    
        public EnemyActionSO actions; //敌人行为表
        public Effect curAction; //当前要执行的行为
        public Player player;

    
        protected override void Awake()
        {
            base.Awake();
            player = FindFirstObjectByType<Player>();
            
            //拷贝一份SO文件 防止多个敌人共享一份SO
            this.hp = ScriptableObject.Instantiate(hp);
            this.defense = ScriptableObject.Instantiate(defense);
            this.buffRound = ScriptableObject.Instantiate(buffRound);
            
            //进入敌人队列
            EnemyManager.Instance.AddEnemy(this);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (isDead)
            {
                EnemyManager.Instance.DisEnemy();
            }
        }

        /// <summary>
        /// 监听 玩家回合开始事件 获得一个action
        /// </summary>
        public void getAction()
        {
            curAction = actions.getAction();
        }
    
        /// <summary>
        /// 对外接口 由EnemyManager调用 执行自身的行为
        /// </summary>
        public void doAction()
        {
            if(isDead) return;
            switch (curAction.targetType)
            {
                case EffectTargetType.Self:
                    skill();
                    break;
                case EffectTargetType.Target:
                    attack();
                    break;
                case EffectTargetType.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void skill()
        {
            StartCoroutine(ProcessDelayAction("skill"));
        }

        private void attack()
        {
            StartCoroutine(ProcessDelayAction("attack"));
        }
    
        /// <summary>
        /// 开启协程
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        IEnumerator ProcessDelayAction(string actionName)
        {
            animator.SetTrigger(actionName);
            yield return new WaitUntil(
                ()=> animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f
                     && !animator.IsInTransition(0)
                     && animator.GetCurrentAnimatorStateInfo(0).IsName(actionName)
            );
            if (actionName == "attack")
            {
                curAction.Execute(this,player);
            }
            else
            {
                curAction.Execute(this,this);
            }
        }
    
    }
}
