using System;
using System.Collections.Generic;
using Card.ScripctsObject;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Panel
{
    public class ChoosePanel : MonoBehaviour
    {  [Header("Ui组件")]
        public Button btnBack; 
        [Header("预制体")]
       public GameObject cardPrefab;
       public GameObject buttonPrefab;

       [Header("卡组数据")] 
       public CardLibrarySO playerCardInfo;
       public CardLibrarySO allCardInfo;

       [Header("布局")] 
       public GameObject layOut;
       
       [FormerlySerializedAs("_cards")] [SerializeField]
       private List<CardLibraryEntry> _cardLibraryEntries = new List<CardLibraryEntry>();
       private List<GameObject> _buttons = new List<GameObject>();
       private List<GameObject> _cards = new List<GameObject>();
       private const int NUM = 3;
       
        private void Awake()
        {
            _buttons = new();
             btnBack.onClick.AddListener((
                 () =>
                 {
                     gameObject.SetActive(false);
                 }
                 ));
        }

        private void OnEnable()
        {
            //获取卡牌
            _cardLibraryEntries = CardGetManager.Instance.getCards(NUM);
            ShowCard();
        }
        
       
        private void ShowCard()
        {
            for (int i = 0; i < NUM; i++)
            {
                var cardObj = Instantiate(cardPrefab, layOut.transform, false);
                var cardView = cardObj.GetComponent<CardView>();
                if (cardView != null)
                {
                    cardView.Init(_cardLibraryEntries[i].cardData);
                }
                _cards.Add(cardObj);
                var buttonRoot = cardObj.transform.Find("BtnPostion");
                var buttonObj = Instantiate(buttonPrefab, buttonRoot ?? cardObj.transform, false);
                _buttons.Add(buttonObj);
            }
            

            foreach (var buttonObj in _buttons)
            {
                var btn = buttonObj.GetComponent<Button>();

                btn.onClick.AddListener(() =>
                {
                    foreach (var otherObj in _buttons)
                    {
                        var otherBtn = otherObj.GetComponent<Button>();
                        // 除了当前按钮之外的全部禁用
                        if (otherBtn != btn)
                        {
                            otherBtn.interactable = false;
                        }
                    }
                    btn.interactable = false;
                });
            }
        }

        private void OnDisable()
        {
            _cardLibraryEntries.Clear();
            foreach (var card in _cards)
            {
                Destroy(card.gameObject);
            }
            _cards.Clear();
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons.Clear();
        }
    }
}