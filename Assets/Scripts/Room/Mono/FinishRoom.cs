using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    public ObjectEventSO loadRoomEvent;
    
    private void OnMouseDown()
    {
        loadRoomEvent.RaiseEvent(null, this);
    }
}
