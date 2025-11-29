using Character;
using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "DefenseEffect", menuName = "Card Effect/DefenseEffect")]
    public class DefenseEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            switch (targetType)
            {
                case EffectTargetType.Self:
                    from.UpdateDefense(value);
                    break;
                case EffectTargetType.Target:
                    to.UpdateDefense(value);
                    break;
                case EffectTargetType.All:
                    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(var enemy in enemies)
                    {
                        enemy.GetComponent<CharacterBase>().UpdateDefense(value);
                    }
                    break;
            }
        }
    }
}
