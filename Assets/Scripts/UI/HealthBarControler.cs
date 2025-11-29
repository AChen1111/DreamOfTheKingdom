using Character;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI
{
    public class HealthBarControler : MonoBehaviour
    {
        public Transform barTransform;
        public Sprite buffSprite;
        [FormerlySerializedAs("deBuff")] public Sprite deBuffSprite;
    
        private CharacterBase currentCharacter;
        private UIDocument healthBarUI;
        private ProgressBar healthBar;
        private VisualElement defense;
        private Label defenseLabel;
        private VisualElement buffIcon;
        void Awake()
        {
            currentCharacter = GetComponent<CharacterBase>();
            Init();
        }

        private void SetPositionInWorld(VisualElement element, Vector3 worldPosition,Vector2 size)
        {
            Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(
                element.panel, worldPosition, size,Camera.main
            );
            element.transform.position = rect.position;
        }

        [ContextMenu("测试位置")]
        private void Init()
        {
            healthBarUI = GetComponent<UIDocument>();
            var root = healthBarUI.rootVisualElement;
            healthBar = root.Q<ProgressBar>("HealthBar");
            healthBar.highValue = currentCharacter.MaxHp;
            defense = healthBar.Q<VisualElement>("Defense");
            defenseLabel = defense.Q<Label>("defenseNum");
            SetPositionInWorld(healthBar, barTransform.position, Vector2.zero);
            defense.style.display = DisplayStyle.None;
            buffIcon = healthBar.Q<VisualElement>("Buff");
        }

        void Update()
        {
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if(currentCharacter.isDead)
            {
                healthBar.style.display = DisplayStyle.None;
                return;
            }
            healthBar.title = $"{currentCharacter.CurrentHP}/{currentCharacter.MaxHp}";
            healthBar.value = currentCharacter.CurrentHP;
        
            healthBar.RemoveFromClassList("highHealth");
            healthBar.RemoveFromClassList("midHealth");
            healthBar.RemoveFromClassList("lowHealth");
        
            //计算百分比
            float progress = (float)currentCharacter.CurrentHP / currentCharacter.MaxHp;
            if (progress < 0.3)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if (progress < 0.6)
            {
                healthBar.AddToClassList("midHealth");
            }
            else
            {
                healthBar.AddToClassList("highHealth");
            }
        
            //防御值显示部分
            defense.style.display = currentCharacter.defense.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            defenseLabel.text = currentCharacter.defense.currentValue.ToString();
        
            //buff显示部分
            switch (currentCharacter.getBuffState())
            {
                case 0:
                    buffIcon.style.display = DisplayStyle.None;
                    break;
                case 1:
                    buffIcon.style.backgroundImage = new StyleBackground(buffSprite);
                    buffIcon.style.display = DisplayStyle.Flex;
                    break;
                case -1:
                    buffIcon.style.backgroundImage = new StyleBackground(deBuffSprite);
                    buffIcon.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}
