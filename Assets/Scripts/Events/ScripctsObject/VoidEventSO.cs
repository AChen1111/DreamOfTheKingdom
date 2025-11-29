using System;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScripctsObject
{
    [CreateAssetMenu(fileName = "VoidEventSO", menuName = "Events/VoidEventSO", order = 0)]
    public class VoidEventSO : ScriptableObject
    {
        /// <summary>
        /// Description of the event.
        /// </summary>
        [TextArea]
        public string description;
        /// <summary>
        /// 泛型委托
        /// </summary>
        public UnityAction OnEventRaised;
        /// <summary>
        /// 最后发送事件的对象
        /// </summary>
        public string lastSender;
        public void RaiseEvent(object sender = null)
        {
            lastSender = sender?.ToString();
            OnEventRaised?.Invoke();
        }
    }
}