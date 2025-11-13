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
    public TextMeshPro cardCost;

    [Header("卡牌数据")]
    public CardDataSO cardData;
    [Header("原始数据")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originalSortingLayer;

    [Header("事件广播")]
    public ObjectEventSO discardEvent;
    public IntEventSO costEvent;
    
    [Header("状态")]
    public bool isMoveing;
    public bool isAvailable;//是否可用
    public Player player;
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
        cardCost.text = cardData.cost.ToString();
        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "技能",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
    
    /// <summary>
    /// 重置位置
    /// </summary>
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


    /// <summary>
    /// 执行卡牌效果
    /// </summary>
    /// <param name="from">使用者</param>
    /// <param name="to">目标</param>
    public void ExecuteEffect(CharacterBase from,CharacterBase to)
    {
        //如果cost 不足返回
        if (!isAvailable) return;
        
        //广播 卡牌产生费用事件
        costEvent.RaiseEvent(cardData.cost,this);
        //触发回收事件
        discardEvent.RaiseEvent(this,this);
        foreach(var effect in cardData.effects)
        {
            effect.Execute(from, to);
        }
    }
    
    
    /// <summary>
    /// 根据cost更新状态
    /// </summary>
    public void UpdateCardState()
    {
        isAvailable = cardData.cost <= player.CurrentMana;
        
        cardCost.color = isAvailable ? Color.green : Color.red;
    }
}