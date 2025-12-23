using Character;
using Character.Enemy;
using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect", order = 0)]
    public class DamageEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            
            int atk = (int) (value * from.AtkBase);
            switch (targetType)
            {
                case EffectTargetType.Self:
                    break;
                case EffectTargetType.Target:
                    if (to == null) return;
                    to.TakeDamage(atk);
                    break;
                case EffectTargetType.All:
                {
                    var enemies = EnemyManager.Instance.GetAllEnemies();

                    var snapshot = enemies.ToArray(); // 需要 using System.Linq;

                    foreach (var enemy in snapshot)
                    {
                        if (enemy == null) continue;
                        var cb = enemy.GetComponent<CharacterBase>();
                        if (cb == null) continue;

                        cb.TakeDamage(atk);
                    }
                    break;
                }
            }
        }
    }
}