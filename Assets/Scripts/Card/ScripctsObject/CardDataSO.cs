using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public int cost;//卡牌费用
    [TextArea]
    public string description;//描述
    public CardType cardType;

    [Header("具体效果列表")]
    public List<Effect> effects;
}

