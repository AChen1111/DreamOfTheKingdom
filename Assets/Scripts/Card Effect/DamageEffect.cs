using Character;
using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect", order = 0)]
    public class DamageEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            if (to == null) return;
            int atk = (int) (value * from.AtkBase);
            switch (targetType)
            {
                case EffectTargetType.Self:
                    break;
                case EffectTargetType.Target:
                    to.TakeDamage(atk);
                    break;
                case EffectTargetType.All:
                    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(var enemy in enemies)
                    {
                        enemy.GetComponent<CharacterBase>().TakeDamage(atk);
                    }
                    break;
            }
        }
    }
}