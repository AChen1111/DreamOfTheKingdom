using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    /// <summary>
    /// 是否为横向布局
    /// </summary>
    [Header("布局方式")]
    public bool isHorizontal;
    [Header("横向参数")]
    public float maxWidth = 7f;
    public float cardSpacing = 2f;
    [Header("扇形参数")]
    // 扇形布局时每张牌之间的夹角
    public float maxAngle = 50f;
    public float angleBetweenCards = 7f;
    // 扇形布局时的半径
    public float radius = 17f;
    [SerializeField] private List<Vector3> cardPositions = new();
    private List<Quaternion> cardRotations = new();
    public Vector3 centerPos;
    
    void Awake()
    {
        centerPos = isHorizontal ? new Vector3(0f, -4.5f, 0f) : new Vector3(0f, -21.5f, 0f);
    }
    public CardTransform GetCardTransform(int index,int totalCards)
    {
        CalculatePosition(totalCards, isHorizontal);
        return new CardTransform(cardPositions[index], cardRotations[index]);
    }
    /// <summary>
    /// 计算牌的位置
    /// </summary>
    /// <param name="cardNum">卡牌数量</param>
    /// <param name="horizontal">是否为横向布局</param>
    private void CalculatePosition(float cardNum, bool horizontal)
    {
        cardPositions.Clear();
        cardRotations.Clear();
        if (horizontal)
        {
            float currentWidth = cardSpacing * (cardNum - 1);
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            float currentSpacing = totalWidth > 0 ? totalWidth / (cardNum - 1) : 0;
            for (int i = 0; i < cardNum; i++)
            {
                float xPos = 0 - (totalWidth / 2) + (i * currentSpacing);

                //将对应旋转加入旋转数组
                var rotation = Quaternion.identity;
                cardRotations.Add(rotation);

                //将对应位置加入位置数组
                var pos = new Vector3(xPos, centerPos.y, 0f);
                cardPositions.Add(pos);
            }
        }
        else
        {
            float cardAngle = (cardNum - 1) * angleBetweenCards / 2;
            float currentAngle = Mathf.Min(cardAngle, maxAngle);
            float currentBetweenAngle = cardNum > 1 ? (currentAngle * 2) / (cardNum - 1) : 0f;

            for (int i = 0; i < cardNum; i++)
            {
                var pos = FanCardPosition(currentAngle - i * currentBetweenAngle);
                var rotation = Quaternion.Euler(0f, 0f, currentAngle - i * currentBetweenAngle);
                cardPositions.Add(pos);
                cardRotations.Add(rotation);
            }
        }
    }
    
    private Vector3 FanCardPosition(float angle)
    {
        return new Vector3(
            centerPos.x - radius * Mathf.Sin(angle * Mathf.Deg2Rad),
            centerPos.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
            0f);
    }
}

