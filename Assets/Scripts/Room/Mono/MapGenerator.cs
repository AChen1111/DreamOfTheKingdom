using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    /// <summary>
    /// 地图配置数据
    /// </summary>
    public MapConfigSO mapConfig;
    /// <summary>
    /// 房间预制体
    /// </summary>
    public Room roomPrefab;

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
    /// 生成的位置
    /// </summary>
    private Vector3 _generatePosition;
    void Awake()
    {
        _screenHeight = Camera.main.orthographicSize * 2f;
        _screenWidth = _screenHeight * Camera.main.aspect;

        _columnWidth = _screenWidth / (mapConfig.roomBlueprints.Count + 1);
    }
    
    void Start()
    {
        CreateMap();
    }
    public void CreateMap()
    {
        // 根据地图配置生成房间
        for(int c = 0;c<mapConfig.roomBlueprints.Count;c++)
        {
            var blueprint = mapConfig.roomBlueprints[c];
            var roomCount = Random.Range(blueprint.min, blueprint.max + 1);
            for (int i = 0; i < roomCount; i++)
            {
                var room = Instantiate(roomPrefab, transform);
                _generatePosition.x = _columnWidth * (c + 1);
                _generatePosition.y = _screenHeight - (i+1) * (_screenHeight / (roomCount + 1));
                room.transform.localPosition = _generatePosition;
            }
            
        }
    }
}
