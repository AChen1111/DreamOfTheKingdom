using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Rooms.ScriptsObject
{
    [CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
    public class RoomDataSO : ScriptableObject
    {
        public Sprite roomIcon;
        public RoomType roomType;
        public AssetReference sceneToLoad;
    }
}

