using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayout;


    /// <summary>
    /// 完成一个房间后,更新房间的事件
    /// </summary>
    /// <param name="roomVector">当前所在房间的坐标</param>
    public void UpdateMapLayoutData(object value)
    {
        var roomVector = (Vector2Int)value;
        //找到当前房间
        var currentRoom = mapLayout.mapRoomDatas.Find(
            room =>
                room.column == roomVector.x &&
                room.line == roomVector.y
        );
        currentRoom.roomState = RoomState.Visited;

        //找到同列房间
        var sameColumnRooms = mapLayout.mapRoomDatas.FindAll(
            room => room.column == currentRoom.column
        );

        //更新状态
        foreach (var room in sameColumnRooms)
        {
            if (room.line != roomVector.y)
                room.roomState = RoomState.Locked;
        }
        
        //更新当前房间所链接房间的状态
        foreach(var link in currentRoom.linkTo)
        {
            var linkedRoom = mapLayout.mapRoomDatas.Find(
                room =>
                    room.column == link.x &&
                    room.line == link.y
            );
            linkedRoom.roomState = RoomState.Attainable;
        }
    }
}
