using System;
using Character;
using Character.Enemy;
using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "StrengthEffect", menuName = "Card Effect/StrengthEffect")]
    public class StrengthEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            switch (targetType)
            {
                case EffectTargetType.Self:
                    from.UpdateAtkBuffRound(value,true);
                    break;
                case EffectTargetType.Target:
                    to = EnemyManager.Instance.GetRandomEnemy();
                    to.UpdateAtkBuffRound(value,true);
                    break;
                case EffectTargetType.All:
                    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(var enemy in enemies)
                    {
                        enemy.GetComponent<CharacterBase>().UpdateAtkBuffRound(value,true);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
