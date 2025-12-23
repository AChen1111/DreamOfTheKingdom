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
        public CardLibrarySO playerCardLibrarySo;
        
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
            int total = allCardLibrarySo.cardLibraries.Count;

            if (total <= 0) return cardList;
            
            int remaining = total - cardSet.Count;
            if (remaining < nums)
            {
                cardSet.Clear();
            }

            int maxTry = 1000;

            // 先尽量抽“不重复”的
            while (cardList.Count < nums && maxTry-- > 0)
            {
                var card = getCard();
                if (cardSet.Add(card))
                {
                    cardList.Add(card);
                }
            }
            
            maxTry = 1000;
            while (cardList.Count < nums && maxTry-- > 0)
            {
                cardList.Add(getCard());
            }

            return cardList;
        }
        

    }
}