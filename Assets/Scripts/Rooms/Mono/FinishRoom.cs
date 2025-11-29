using Events.ScripctsObject;
using UnityEngine;

namespace Rooms.Mono
{
    public class FinishRoom : MonoBehaviour
    {
        public ObjectEventSO loadRoomEvent;
    
        private void OnMouseDown()
        {
            loadRoomEvent.RaiseEvent(null, this);
        }
    }
}
