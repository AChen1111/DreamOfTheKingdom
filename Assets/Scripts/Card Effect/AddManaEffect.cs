using System;
using Character;
using Tools;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "DefenseEffect", menuName = "Card Effect/AddManaEffect")]
    public class AddManaEffect : Effect
    {
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            Player manaPlayer = from as Player;
            switch (targetType)
            {
                case EffectTargetType.Self:
                    if (manaPlayer != null) manaPlayer.AddMana(value);
                    break;
                case EffectTargetType.Target:
                    break;
                case EffectTargetType.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}