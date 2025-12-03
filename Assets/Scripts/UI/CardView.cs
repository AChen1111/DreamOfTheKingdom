using Card.ScripctsObject;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardView : MonoBehaviour
    {
        [Header("组件")]
        public Image cardSprite;
        public TextMeshProUGUI cardDescription;
        public TextMeshProUGUI cardCost;

        [Header("卡牌数据")]
        [SerializeField] private CardDataSO cardData;

        public void Init(CardDataSO cardDataSO)
        {
            cardData = cardDataSO;
            if (cardData == null) return;

            if (cardSprite != null)
                cardSprite.sprite = cardData.cardImage;
            if (cardDescription != null)
                cardDescription.text = cardData.description;
            if (cardCost != null)
                cardCost.text = cardData.cost.ToString();
        }

        private void Start()
        {
            if (cardData != null)
            {
                Init(cardData);
            }
        }
    }
}