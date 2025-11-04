using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effect/DamageEffect", order = 0)]
public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase to)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                break;
            case EffectTargetType.Target:
                to.TakeDamage(value);
                break;
            case EffectTargetType.All:
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var enemy in enemies)
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }
}