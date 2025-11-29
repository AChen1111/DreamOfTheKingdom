using Tools;
using UnityEngine;

namespace Character
{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Skill = Animator.StringToHash("skill");
        private static readonly int IsSleep = Animator.StringToHash("isSleep");
        private static readonly int IsParry = Animator.StringToHash("isParry");
        private Player player;
        private Animator animator;

        private void Awake()
        { 
            player = GetComponent<Player>();
            animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            animator.Play("sleep");
            animator.SetBool(IsSleep,true);
        }
        
        /// <summary>
        /// 监听 回合开始事件
        /// </summary>
        public void PlayerTurnBeginAnimation()
        {
            animator.SetBool(IsSleep,false);
            animator.SetBool(IsParry,false);
        }
        
        /// <summary>
        /// 监听 回合结束事件
        /// </summary>
        public void PlayerTurnEndAnimation()
        {
            if(player.defense.currentValue > 0)
            {
                animator.SetBool(IsParry,true);
            }
            else
            { 
                animator.SetBool(IsSleep,true);
            }
        }
    
        /// <summary>
        /// 监听 卡牌使用事件
        /// </summary>
        public void OnPlayCardEvent(object obj)
        {
            var card = obj as Card.Mono.Card;
            if (card != null)
                switch (card.cardData.cardType)
                {
                    case CardType.Attack:
                        animator.SetTrigger(Attack);
                        break;
                    case CardType.Defense:
                    case CardType.Abilities:
                        animator.SetTrigger(Skill);
                        break;
                }
        }
        
    }
}
