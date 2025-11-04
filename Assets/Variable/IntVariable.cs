using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable", order = 0)]
public class IntVariable : ScriptableObject {
    public int maxValue;
    public int currentValue;
    /// <summary>
    /// 值改变事件
    /// </summary>
    public IntEventSO ValueChangedEvent;
    
    [TextArea]
    [SerializeField]private string description;

    public void SetValue(int newValue) {
        currentValue = newValue;
        ValueChangedEvent.RaiseEvent(currentValue);
    }
}