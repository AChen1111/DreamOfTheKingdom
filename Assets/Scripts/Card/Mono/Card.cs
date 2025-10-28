using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
    [Header("组件")]
    public TextMeshPro cardName;
    public SpriteRenderer cardSprite;
    public TextMeshPro cardDescription;
    public TextMeshPro cardType;
    [Header("卡牌数据")]
    public CardDataSO cardData;
    void Start()
    {
        Init();
    }
    public void Init()
    {
        cardSprite.sprite = cardData.cardImage;
        cardName.text = cardData.cardName;
        cardDescription.text = cardData.description;

        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "技能",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };
    }
}