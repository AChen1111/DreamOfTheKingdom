using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarControler : MonoBehaviour
{
    public Transform barTransform;

    private CharacterBase currentCharacter;
    private UIDocument healthBarUI;
    private ProgressBar healthBar;
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
        SetPositionInWorld(healthBar, barTransform.position, Vector2.zero);        
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
    }
}
