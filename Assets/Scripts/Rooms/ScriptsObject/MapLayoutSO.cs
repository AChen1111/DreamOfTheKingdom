using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Rooms.ScriptsObject
{
    [CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
    public class MapLayoutSO : ScriptableObject
    {
        public List<MapRoomData> mapRoomDatas = new List<MapRoomData>();
        public List<LinePosition> linePositions = new List<LinePosition>();
        
        public void ResetMapSO()
        {
            mapRoomDatas.Clear();
            linePositions.Clear();
        }
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
        public List<Vector2Int> linkTo;
    }

    [System.Serializable]
    public class LinePosition
    {
        public SerializableVector3 startPos, endPos;
    }
}