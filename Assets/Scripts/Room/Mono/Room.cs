using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int colume;
    public int line;
    public SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo = new();

    [Header("广播")]
    public ObjectEventSO loadRoomEvent;

    private void OnMouseDown()
    {
        Debug.Log("当前类型" + roomData.roomType);
        if(this.roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaiseEvent(this, this);            
        }
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
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
            RoomState.Attainable => Color.white,
            _ => throw new System.NotImplementedException(),
        };
    }

}
