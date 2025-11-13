using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDeck : MonoBehaviour {
    [Header("管理器引用")]
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new();//抽牌堆
    private List<CardDataSO> discardDeck = new();//弃牌堆
    private List<Card> handCardObjectList = new();//当前手牌
    
    public Vector3 DeckPosition;
    
    /// <summary>
    /// 抽牌和弃牌事件
    /// </summary>
    [Header("事件广播")]
    public IntEventSO DrawCardEvent;
    public IntEventSO DisCardEvent;

/// <summary>
/// !!!!!测试用
/// </summary>
    void Start()
    {
        InitDeck();
    }

    //初始化牌堆
    public void InitDeck()
    {
        drawDeck.Clear();
        foreach (var entry in cardManager.currentLibrary.cardLibraries)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        //TODO:洗牌逻辑
        ShuffleDeck();
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount">需要抽出牌的数量</param>
    private void DrawCard(int amount)
    {
        if (drawDeck.Count < amount)
        {
            foreach (var _card in discardDeck)
            {
                drawDeck.Add(_card);
            }
            ShuffleDeck();
        }
        
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                Debug.Log("牌堆为空");
                return;
            }
            CardDataSO currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);

            var card = cardManager.GetCardObject().GetComponent<Card>();
            card.Init(currentCardData);
            card.transform.position = DeckPosition;
            handCardObjectList.Add(card);
            float delay = i * 0.1f;
            SetCardLayOut(delay);
            DrawCardEvent.RaiseEvent(drawDeck.Count,this);//广播 牌堆的数量变化
        }
        
    }


    private void SetCardLayOut(float delay)
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            var currentCard = handCardObjectList[i];
            var cardTransform = cardLayoutManager.GetCardTransform(i, handCardObjectList.Count);
            currentCard.UpdateCardState();
            currentCard.isMoveing = true;
            ///抽牌动画
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).OnComplete(() =>
            {
                currentCard.transform.DOMove(cardTransform.pos, 0.5f).OnComplete(() =>
                {
                    currentCard.isMoveing = false;
                });
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            });
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            currentCard.SavePosition(cardTransform.pos, cardTransform.rotation);
        }
    }
    /// <summary>
    /// 洗牌
    /// </summary>
    private void ShuffleDeck()
    {
        discardDeck.Clear();
        
        //Ui显示变化
        DrawCardEvent.RaiseEvent(drawDeck.Count,this);
        DisCardEvent.RaiseEvent(discardDeck.Count,this);
        
        for (int i = 0; i < drawDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, drawDeck.Count);
            (drawDeck[i], drawDeck[randomIndex]) = (drawDeck[randomIndex], drawDeck[i]);
        }
    }

    /// <summary>
    /// 弃牌逻辑,事件函数
    /// </summary>
    /// <param name="card"></param>
    public void DisCard(object obj)
    {
        Card card = obj as Card;
        discardDeck.Add(card.cardData);
        
        handCardObjectList.Remove(card);
        DisCardEvent.RaiseEvent(discardDeck.Count,this);//广播 弃牌堆的数量变化
        cardManager.DisCardObject(card.gameObject);
        SetCardLayOut(0f);
    }

    /// <summary>
    /// 监听玩家回合开始事件
    /// </summary>
    public void PlayerTurnBegin()
    {
        DrawCard(3);
    }
    
    /// <summary>
    /// 监听玩家回合结束事件
    /// </summary>
    public void PlayerTurnEnd()
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            var card = handCardObjectList[i];
            discardDeck.Add(card.cardData);
            
            //弃牌动画
            card.isMoveing = true;
            card.transform.DOScale(Vector3.zero, 0.2f).OnComplete(
                ()=>{cardManager.DisCardObject(card.gameObject);}
                );
        }
        handCardObjectList.Clear();
        DisCardEvent.RaiseEvent(discardDeck.Count,this);//广播 弃牌堆的数量变化
    }
    
    //!!!测试用
    [ContextMenu("抽牌测试")]
    public void TestDrawCard()
    {
        DrawCard(1);
    }
}