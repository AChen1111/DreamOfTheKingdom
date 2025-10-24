using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/RoomDataSO")]
public class RoomDataSO : MonoBehaviour
{
    public Sprite roomIcon;
    public RoomType roomType;
    public AssetReference roomPrefab;
}

