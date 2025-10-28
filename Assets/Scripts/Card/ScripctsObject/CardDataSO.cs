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

    //todo:具体效果
}

