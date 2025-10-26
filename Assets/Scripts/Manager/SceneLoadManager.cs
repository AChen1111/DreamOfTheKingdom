using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    public void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
           Debug.Log("加载房间数据:" + ((RoomDataSO)data).roomType);
        }
    }
}

