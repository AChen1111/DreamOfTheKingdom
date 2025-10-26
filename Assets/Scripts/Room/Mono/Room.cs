using UnityEngine;

public class Room : MonoBehaviour
{
    public int colume;
    public int line;
    public SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;

    [Header("广播")]
    public ObjectEventSO loadRoomEvent;

    private void OnMouseDown()
    {
        Debug.Log("当前类型" + roomData.roomType);
        loadRoomEvent.RaiseEvent(roomData, this);
    }

/// <summary>
/// 外部调用,用来设置房间数据
/// </summary>
/// <param name="colume"></param>
/// <param name="line"></param>
/// <param name="data"></param>
    public void SetUpRoom(int colume, int line, RoomDataSO data)
    {
        this.colume = colume;
        this.line = line;
        roomData = data;
        spriteRenderer.sprite = roomData.roomIcon;
    }

}
