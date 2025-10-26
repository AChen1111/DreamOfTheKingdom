using UnityEngine;
using UnityEngine.Events;

public class BaseEventSO<T> : ScriptableObject
{
    /// <summary>
    /// Description of the event.
    /// </summary>
    [TextArea]
    public string description;
    /// <summary>
    /// 泛型委托
    /// </summary>
    public UnityAction<T> OnEventRaised;
    /// <summary>
    /// 最后发送事件的对象
    /// </summary>
    public string lastSender;
    public void RaiseEvent(T value,object sender = null)
    {
        lastSender = sender?.ToString();
        OnEventRaised?.Invoke(value);
    }
}
