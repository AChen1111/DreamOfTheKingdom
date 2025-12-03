using System.Collections.Generic;
using Card.ScripctsObject;
using UnityEngine;

namespace Manager
{
    
    public class CardGetManager : MonoBehaviour
    {
        //单例
        public static CardGetManager Instance{get; private set;}
        private HashSet<CardLibraryEntry> cardSet = new ();//设置集合保证取出结果不一致
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
        public CardLibrarySO allCardLibrarySo;
        
        /// <summary>
        /// 获取随机
        /// </summary>
        /// <returns></returns>
        private CardLibraryEntry getCard()
        {
            int r = Random.Range(0,allCardLibrarySo.cardLibraries.Count);
            var card = allCardLibrarySo.cardLibraries[r];
            return card;
        }

        /// <summary>
        /// 对外接口 
        /// </summary>
        /// <returns>lib列表</returns>
        public List<CardLibraryEntry> getCards(int nums)
        {
            var cardList = new List<CardLibraryEntry>();
            for (int i = 0; i < nums; i++)
            {
                var card = getCard();
                if (!cardSet.Contains(card))
                {
                    cardList.Add(card);
                    cardSet.Add(card);
                }
                else
                {
                    i--;
                }
                
            }
            return cardList;
        }
    }
}