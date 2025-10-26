using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// 当前场景的引用
    /// </summary>
    private AssetReference _currentScene;
    /// <summary>
    /// 地图的引用
    /// </summary>
    public AssetReference map;

    /// <summary>
    /// 监听房间加载事件
    /// </summary>
    /// <param name="data"></param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            var currentData = data as RoomDataSO;
            _currentScene = currentData.sceneToLoad;
        }
        //异步

        await UnloadCurrentSceneTask();
        await LoadSceneTask();
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    private async Awaitable LoadSceneTask()
    {
        var s = _currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }

    private async Awaitable UnloadCurrentSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    /// <summary>
    /// 监听地图加载事件
    /// </summary>
    public async void LoadMap()
    {
        await UnloadCurrentSceneTask();
        _currentScene = map;
        await LoadSceneTask();
    }
}

