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
            if (targetType == EffectTargetType.Self)
            {
                from.HealHealth(value);
            }
            else if (targetType == EffectTargetType.Target)
            {
                to = EnemyManager.Instance.GetRandomEnemy();
                to.HealHealth(value);
            }
        }
    }
}
