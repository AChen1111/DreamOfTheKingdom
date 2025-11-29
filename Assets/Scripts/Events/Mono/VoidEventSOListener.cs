using Events.ScripctsObject;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Mono
{
    public class VoidEventSOListener: MonoBehaviour
    {
        public VoidEventSO eventSO;

        /// <summary>
        /// 回调函数列表
        /// </summary>
        public UnityEvent response;

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

        private void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}