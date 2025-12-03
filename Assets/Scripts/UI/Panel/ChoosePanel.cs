using System;
using System.Collections.Generic;
using Card.ScripctsObject;
using Manager;
using UnityEngine;
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
       
       [SerializeField]
       private List<CardLibraryEntry> _cards;
       private List<GameObject> _buttons;
       
        private void Awake()
        {
            _cards = new();
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
            _cards = CardGetManager.Instance.getCards(3);
        }
        
        //todo:Ui部分
        private void ShowCard()
        {
            
        }
        private void OnDisable()
        {
            _cards.Clear();
        }
    }
}