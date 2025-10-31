using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
    [Header("组件")]
    public TextMeshPro cardName;
    public SpriteRenderer cardSprite;
    public TextMeshPro cardDescription;
    public TextMeshPro cardType;

    [Header("卡牌数据")]
    public CardDataSO cardData;
    [Header("原始数据")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originalSortingLayer;

    public bool isMoveing;
    void Start()
    {
        Init(cardData);
    }
    public void Init(CardDataSO cardDataSO)
    {
        cardData = cardDataSO;
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

    /// <summary>
    /// 保存当前位置和旋转
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public void SavePosition(Vector3 pos, Quaternion rot)
    {
        originalPosition = pos;
        originalRotation = rot;
        originalSortingLayer = GetComponent<SortingGroup>().sortingOrder;
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        GetComponent<SortingGroup>().sortingOrder = originalSortingLayer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isMoveing) return;
        ResetPosition();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isMoveing) return;
        transform.position = originalPosition + Vector3.up;
        transform.rotation = Quaternion.identity;
    }
}