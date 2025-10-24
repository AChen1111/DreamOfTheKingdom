using UnityEngine;

public class Room : MonoBehaviour
{
    public int colume;
    public int line;
    private SpriteRenderer _spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SetUpRoom(colume, line, roomData);
    }

    private void OnMouseDown()
    {
        Debug.Log("当前类型" + roomData.roomType);
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
        _spriteRenderer.sprite = roomData.roomIcon;
    }

}
