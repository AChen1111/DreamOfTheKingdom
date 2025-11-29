using Character;
using Events.ScripctsObject;
using UnityEngine;

namespace Card_Effect
{
    [CreateAssetMenu(fileName = "DrawCardEffect", menuName = "Card Effect/DrawCardEffect")]
    public class DrawCardEffect : Effect
    {
        public IntEventSO DrawCardEffectEvent;
        public override void Execute(CharacterBase from, CharacterBase to)
        {
            DrawCardEffectEvent?.RaiseEvent(value);
        }
    }
}
