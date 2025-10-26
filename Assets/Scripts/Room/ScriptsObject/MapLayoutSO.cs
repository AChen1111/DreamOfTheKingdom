using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject
{
    public List<MapRoomData> mapRoomDatas = new List<MapRoomData>();
    public List<LinePosition> linePositions = new List<LinePosition>(); 
}


/// <summary>
/// 需要保存的数据
/// </summary>
[System.Serializable]
public class MapRoomData
{
    //世界坐标
    public float posX, posY;
    //抽象坐标
    public int column, line;
    public RoomDataSO roomDataSO;
    public RoomState roomState;
}

[System.Serializable]
public class LinePosition
{
    public SerializableVector3 startPos, endPos;
}