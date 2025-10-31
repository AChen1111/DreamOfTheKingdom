using UnityEngine;

/// <summary>
/// 卡牌Transform值
/// </summary>
public struct CardTransform
{
    public Vector3 pos;
    public Quaternion rotation;
    public CardTransform(Vector3 pos, Quaternion rotation)
    {
        this.pos = pos;
        this.rotation = rotation;
    }

}