 using Character;
 using Character.Enemy;
 using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "Card Effect/HealEffect")]
    public class HealEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            //Debug.Log($"[HealEffect] value={value}, targetType={targetType}, from={from.name}, to={to?.name}");
            //Debug.Log($"before heal: hp={from.CurrentHP}, maxHp={from.MaxHp}, value={value}");

            if (targetType == EffectTargetType.Self)
            {
                from.HealHealth(value);
            }
            else if (targetType == EffectTargetType.Target)
            {
                to = EnemyManager.Instance.GetRandomEnemy();
                to.HealHealth(value);
            }
            else if (targetType == EffectTargetType.All)
            {
                foreach (var enemy in EnemyManager.Instance.GetAllEnemies())
                {
                    enemy.HealHealth(value);
                }
            }
        }
    }
}
