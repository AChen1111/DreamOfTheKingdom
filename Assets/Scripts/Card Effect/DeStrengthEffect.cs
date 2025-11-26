using System;
using UnityEngine;
[CreateAssetMenu(fileName = "DeStrengthEffect", menuName = "Card Effect/DeStrengthEffect")]
public class DeStrengthEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase to)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                from.UpdateAtkBuffRound(value,false);
                break;
            case EffectTargetType.Target:
                to.UpdateAtkBuffRound(value,false);
                break;
            case EffectTargetType.All:
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var enemy in enemies)
                {
                    enemy.GetComponent<CharacterBase>().UpdateAtkBuffRound(value,false);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}