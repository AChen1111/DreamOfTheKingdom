using System;
using UnityEngine;

/// <summary>
/// 所有卡牌效果的基类
/// </summary>
public abstract class Effect : ScriptableObject
{
    public int value;
    public EffectTargetType targetType;

    public abstract void Execute(CharacterBase from, CharacterBase to);
}
