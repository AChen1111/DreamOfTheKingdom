
using System.Collections;
using System.Collections.Generic;
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
        /// 协程 执行每一个敌人的方法
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
        }
        
        /// <summary>
        /// 监听 玩家胜利方法 清空这个列表
        /// </summary>
        public void ClearEnemies()
        {
            _enemies.Clear();
        }
        
        /// <summary>
        /// 敌人死亡时 调用
        /// </summary>
        public void DisEnemy()
        {
            count--;
            if (count == 0)
            {
                //todo:广播玩家胜利事件
            }
        }
    }
}