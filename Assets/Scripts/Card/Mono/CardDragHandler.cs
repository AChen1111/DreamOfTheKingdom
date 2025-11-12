
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 卡牌拖拽
/// </summary>
public class CardDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;
    private GameObject currentArrow;
    private Card currentCard;
    private bool canMove;
    private bool canExecute;
    private CharacterBase targetCharacter;
    
    private void Awake() {
        currentCard = GetComponent<Card>();
    }
    
    /// <summary>
    /// 在回收时初始化状态
    /// </summary>
    private void OnDisable()
    {
        canMove = false;
        canExecute = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canMove)
        {
            currentCard.isMoveing = true;
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            if (Camera.main != null)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint);

                currentCard.transform.position = worldPosition;
                canExecute = worldPosition.y > 1f;
            }
            else
            {
                Debug.Log("Can't find card");
            }
        }
        else
        {
            if(eventData.pointerEnter == null)
            { 
                return;
            }
            if(eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            canExecute = false;
            targetCharacter = null;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(
                    arrowPrefab, currentCard.transform.position, Quaternion.identity
                    );
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }

        if (canExecute)
        {
            currentCard.ExecuteEffect(currentCard.player, targetCharacter);
        }

        else
        {
            //回到原始位置
            currentCard.ResetPosition();
            currentCard.isMoveing = false;
        }
    }
}
