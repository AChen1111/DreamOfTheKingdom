using System;
using UnityEngine;
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
