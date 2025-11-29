using System;
using Events.ScripctsObject;
using UnityEngine;

namespace Rooms.Mono
{
    public class FinishRoom : MonoBehaviour
    {
        public ObjectEventSO loadRoomEvent;
    
        public void OnFinishRoom()
        {
            loadRoomEvent.RaiseEvent(null, this);
        }

        public void OnMouseDown()
        {
            loadRoomEvent.RaiseEvent(null, this);
        }
    }
}
