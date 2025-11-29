
using System.Collections;
using System.Collections.Generic;
using Events.ScripctsObject;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get;private set; }
        [SerializeField]
        private List<Enemy> _enemies = new ();
        [SerializeField]
        private int count = 0;
        
        [Header("事件广播")]
        public ObjectEventSO EnemyTurnOverEvent;
        public VoidEventSO PlayerWinEvent;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            count++;
        }
        
        
        /// <summary>
        /// 监听 敌人回合开始事件 执行敌人的action
        /// </summary>
        public void OnEnemyTurnBegin()
        {
            StartCoroutine(doActions());
        }
        
        /// <summary>
        /// 协程方法 执行每一个敌人的方法
        /// </summary>
        /// <returns></returns>
        IEnumerator doActions()
        {
            foreach (var enemy in  _enemies)
            {
                enemy.doAction();
                //等待1s后执行 另一个敌人的action
                yield return  new WaitForSeconds(1f);
            }
            EnemyTurnOverEvent.RaiseEvent(this,this);
        }
        
        /// <summary>
        /// 当玩家胜利时调用 清空这个列表
        /// </summary>
        private void ClearEnemies()
        {
            _enemies.Clear();
        }
        
        /// <summary>
        /// 全部敌人死亡时 调用
        /// </summary>
        public void DisEnemy()
        {
            count--;
            if (count == 0)
            {
                //广播玩家胜利事件
                PlayerWinEvent.RaiseEvent(this);
                ClearEnemies();
            }
        }
        
        /// <summary>
        /// 获得一个随机的敌人
        /// </summary>
        /// <param name="isAlive">是否要求为存活的敌人</param>
        /// <returns>敌人的引用</returns>
        public Enemy GetRandomEnemy(bool isAlive = true)
        {
            int index = Random.Range(0, _enemies.Count);
            if(!isAlive) return _enemies[index];
            while (_enemies[index].isDead)
            {
                index = Random.Range(0, _enemies.Count);
            }
            return _enemies[index];
        }
    }
}