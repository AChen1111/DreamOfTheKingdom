using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Rooms.ScriptsObject
{
    [CreateAssetMenu(fileName = "MapConfigSO", menuName = "Map/MapConfigSO")]
    public class MapConfigSO : ScriptableObject
    {
        public List<RoomBlueprint> roomBlueprints;
    }

    /// <summary>
    /// 房间蓝图
    /// </summary>
    [System.Serializable]
    public class RoomBlueprint
    {
        /// <summary>
        /// 最小数量,最大数量
        /// </summary>
        public int min, max;
        public RoomType roomType;
    }
}