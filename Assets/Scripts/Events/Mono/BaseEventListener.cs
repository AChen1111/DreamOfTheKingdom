using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;

    /// <summary>
    /// 回调函数列表
    /// </summary>
    public UnityEvent<T> response;

    void OnEnable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised += OnEventRaised;
    }

    void OnDisable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised -= OnEventRaised;
    }

    private void OnEventRaised(T value)
    {
        response?.Invoke(value);
    }
}
