using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    /// <summary>
    /// 地图配置数据
    /// </summary>
    [Header("地图配置数据")]
    public MapConfigSO mapConfig;

    [Header("地图布局数据")]
    public MapLayoutSO mapLayout;

    /// <summary>
    /// 房间预制体
    /// </summary>
    [Header("房间预制体")]
    public Room roomPrefab;

    [Header("线预制体")]
    public LineRenderer lineRenderer;
    /// <summary>
    /// 获取屏幕宽高
    /// </summary>
    private float _screenHeight;
    private float _screenWidth;

    /// <summary>
    /// 每列宽度
    /// </summary>
    private float _columnWidth;

    /// <summary>
    /// 预留边界
    /// </summary>
    public float board;

    /// <summary>
    /// 生成位置
    /// </summary>
    private Vector3 _generatePosition;

    /// <summary>
    /// 已生成的房间列表和连线列表
    /// </summary>
    private List<Room> _rooms = new List<Room>();
    private List<LineRenderer> _lines = new List<LineRenderer>();

    /// <summary>
    /// 房间数据列表
    /// </summary>
    public List<RoomDataSO> roomDataList = new List<RoomDataSO>();
    private Dictionary<RoomType,RoomDataSO> _roomDataDictionary = new Dictionary<RoomType, RoomDataSO>();

    void Awake()
    {
        _screenHeight = Camera.main.orthographicSize * 2f;
        _screenWidth = _screenHeight * Camera.main.aspect;

        _columnWidth = _screenWidth / (mapConfig.roomBlueprints.Count + 1);

        foreach (var roomData in roomDataList)
        {
            _roomDataDictionary[roomData.roomType] = roomData;
        }
    }

    // void Start()
    // {
    //     CreateMap();
    // }
    void OnEnable()
    {
        if (mapLayout.mapRoomDatas.Count > 0)
        {
            LoadMapLayout();
        }
        else
        {
            CreateMap();
        }
    }

    /// <summary>
    /// 创建地图
    /// </summary>
    public void CreateMap()
    {
        //前一列的房间列表
        List<Room> preColumnRooms = new List<Room>();
        // 根据地图配置生成房间
        for (int c = 0; c < mapConfig.roomBlueprints.Count; c++)
        {
            // 当前列的房间列表
            List<Room> currentColumnRooms = new List<Room>();
            var blueprint = mapConfig.roomBlueprints[c];

            var roomCount = Random.Range(blueprint.min, blueprint.max + 1);

            var startHeight = _screenHeight / 2 - _screenHeight / (roomCount + 1);

            _generatePosition = new Vector3(-_screenWidth / 2 + board + _columnWidth * c, startHeight, 0);

            var newPosition = _generatePosition;

            //第c列房间间隔
            var roomGapY = _screenHeight / (roomCount + 1);
            //在第C列生成房间
            for (int i = 0; i < roomCount; i++)
            {
                //预留boss房间的位置
                if (c == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = _screenWidth / 2 - board * 2;
                }
                //增加凌乱感
                else if (c != 0)
                {
                    newPosition.x = _generatePosition.x + Random.Range(-board / 2, board / 2);
                }
                //计算当前房间的位置
                newPosition.y = startHeight - i * roomGapY;

                // 实例化房间
                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);
                var newType = GetRandomRoomType(mapConfig.roomBlueprints[c].roomType);
                // 设置房间数据
                room.SetUpRoom(c, i, _roomDataDictionary[newType]);
                _rooms.Add(room);
                currentColumnRooms.Add(room);
            }

            // 创建连线
            if (preColumnRooms.Count > 0)
            {
                CreateConnectionsInColumn(preColumnRooms, currentColumnRooms);
            }
            preColumnRooms = currentColumnRooms;
        }
        //保存当前布局
        SaveMapLayout();
    }

    /// <summary>
    /// 把Map装箱
    /// </summary>
    private void SaveMapLayout()
    {
        mapLayout.mapRoomDatas.Clear();
        foreach (var room in _rooms)
        {
            MapRoomData mapRoomData = new MapRoomData
            {
                posX = room.transform.position.x,
                posY = room.transform.position.y,
                column = room.colume,
                line = room.line,
                roomDataSO = room.roomData,
                roomState = room.roomState
            };
            mapLayout.mapRoomDatas.Add(mapRoomData);
        }

        mapLayout.linePositions.Clear();
        foreach (var line in _lines)
        {
            LinePosition linePosition = new LinePosition
            {
                startPos = new SerializableVector3(line.GetPosition(0)),
                endPos = new SerializableVector3(line.GetPosition(1))
            };
            mapLayout.linePositions.Add(linePosition);
        }
    }
    
    /// <summary>
    /// 把Map解包
    /// </summary>
    private void LoadMapLayout()
    {
        foreach (var mapRoomData in mapLayout.mapRoomDatas)
        {
            var newPos = new Vector3(mapRoomData.posX, mapRoomData.posY);
            var room = Instantiate(roomPrefab, newPos, Quaternion.identity, transform);
            room.SetUpRoom(mapRoomData.column, mapRoomData.line, mapRoomData.roomDataSO);
            room.roomState = mapRoomData.roomState;
            _rooms.Add(room);
        }
        
        foreach(var lineData in mapLayout.linePositions)
        {
            var line = Instantiate(lineRenderer, transform);
            line.SetPosition(0, lineData.startPos.ToVector3());
            line.SetPosition(1, lineData.endPos.ToVector3());
            _lines.Add(line);
        }
    }


    /// <summary>
    /// 创建列之间的连线
    /// </summary>
    /// <param name="preColumnRooms"></param>
    /// <param name="currentColumnRooms"></param>
    private void CreateConnectionsInColumn(List<Room> preColumnRooms, List<Room> currentColumnRooms)
    {
        /// 记录已连接的当前列房间，防止遗漏
        HashSet<Room> connectedCurrentRooms = new HashSet<Room>();
        foreach (var preRoom in preColumnRooms)
        {
            var targetRoom = CreateConnectionInRoom(preRoom, currentColumnRooms);
            connectedCurrentRooms.Add(targetRoom);
        }
        //如果有未连接的房间，反向链接这个房间
        foreach (var currentRoom in currentColumnRooms)
        {
            if (!connectedCurrentRooms.Contains(currentRoom))
            {
                var fromRoom = preColumnRooms[Random.Range(0, preColumnRooms.Count)];
                CreateConnectionInRoom(fromRoom, new List<Room> { currentRoom });
            }
        }
    }
    /// <summary>
    /// 获取距离指定房间最近的房间
    /// </summary>
    private Room CreateConnectionInRoom(Room fromRoom, List<Room> toRooms)
    {
        Room targetRoom = toRooms[Random.Range(0, toRooms.Count)];
        var line = Instantiate(lineRenderer, transform);
        line.SetPosition(0, fromRoom.transform.position);
        line.SetPosition(1, targetRoom.transform.position);
        _lines.Add(line);
        return targetRoom;
    }


    [ContextMenu("ReGenerateMap")]
    /// <summary>
    /// 重新生成地图
    /// </summary>
    public void ReGenerateMap()
    {
        // 删除已有房间
        foreach (var room in _rooms)
        {
            Destroy(room.gameObject);
        }
        // 删除已有连线
        foreach (var line in _lines)
        {
            Destroy(line.gameObject);
        }
        _rooms.Clear();

        // 重新创建地图
        CreateMap();
    }

    /// <summary>
    /// 获取房间数据
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    public RoomDataSO GetRoomData(RoomType roomType)
    {
        if (_roomDataDictionary.TryGetValue(roomType, out var roomData))
        {
            return roomData;
        }
        return null;
    }

    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] options = flags.ToString().Split(',');
        string randomOption = options[Random.Range(0, options.Length)];

        return (RoomType)System.Enum.Parse(typeof(RoomType), randomOption);
    }
}
