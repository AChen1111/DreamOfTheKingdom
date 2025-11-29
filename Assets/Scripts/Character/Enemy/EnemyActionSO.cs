using System.Collections.Generic;
using Card_Effect;
using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "EnemyActionSO", menuName = "Enemy/EnemyActionSO", order = 0)]
    public class EnemyActionSO : ScriptableObject
    {
        // 在 Inspector 中直接配置
        public List<EnemyAction> actions = new List<EnemyAction>();

        // 总权重
        private int W = 0;

        private void OnEnable()
        {
            RecalculateTotalWeight();
        }

#if UNITY_EDITOR
        // 在 Inspector 中修改权重/增删元素时，自动刷新总权重
        private void OnValidate()
        {
            RecalculateTotalWeight();
        }
#endif

        /// <summary>
        /// 重新统计一次总权重（比如从 Inspector 中修改后）
        /// </summary>
        private void RecalculateTotalWeight()
        {
            W = 0;
            if (actions == null) return;

            foreach (var action in actions)
            {
                if (action == null) continue;
                if (action.w > 0)
                    W += action.w;
            }
        }   
    
        /// <summary>
        /// 添加一个action
        /// </summary>
        /// <param name="action"></param>
        public void Add(EnemyAction action)
        {
            if (action == null) return;
            if (actions == null) actions = new List<EnemyAction>();

            actions.Add(action);
            if (action.w > 0)
                W += action.w;
        }
    
    
        /// <summary>
        /// 移除一个action
        /// </summary>
        /// <param name="action"></param>
        public void Remove(EnemyAction action)
        {
            if (actions == null || action == null) return;

            // 只有真正删掉了才减权重
            if (actions.Remove(action))
            {
                if (action.w > 0)
                    W -= action.w;
            }
        }

        /// <summary>
        /// 根据权重随机返回一个 Effect，w 越大被选中的概率越大
        /// </summary>
        public Effect getAction()
        {
            if (actions == null || actions.Count == 0)
                return null;

            // 防止 W 异常（例如全是 0 或没刷新）
            if (W <= 0)
            {
                RecalculateTotalWeight();
                if (W <= 0) return null; // 说明所有 w 都 <=0
            }

            // 在 [0, W) 范围随机一个整数
            int r = Random.Range(0, W);
            int sum = 0;

            foreach (var action in actions)
            {
                if (action == null || action.w <= 0) continue;

                sum += action.w;
                if (r < sum)
                {
                    return action.effect;
                }
            }

            // 理论上不会走到这里，兜底返回最后一个有 Effect 的
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (actions[i] != null && actions[i].effect != null)
                    return actions[i].effect;
            }

            return null;
        }
    }

    [System.Serializable]
    public class EnemyAction
    {
        public Effect effect;
        public int w; // 权重，默认 1

        public EnemyAction(Effect e, int w)
        {
            this.effect = e;
            this.w = w;
        }
    }
}