using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScripctsObject;
using Rooms.Mono;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class SceneLoadManager : MonoBehaviour
    {
        /// <summary>
        /// 当前场景的引用
        /// </summary>
        private AssetReference _currentScene;
        /// <summary>
        /// 地图的引用
        /// </summary>
        [Header("场景")]
        public AssetReference map;
        public AssetReference menu;
        public AssetReference intro;
        
        /// <summary>
        /// 当前房间的行列
        /// </summary>
        private Vector2Int currentRoomVector;

        [Header("广播")]
        public ObjectEventSO afterRoomLoadEvent;
        
        /// <summary>
        /// 测试时 不要进入这个函数
        /// </summary>
        private void Awake()
        {
// #if UNITY_EDITOR
//             // 在编辑器里，如果不是正在 Play，就直接返回
//             if (!Application.isPlaying)
//                 return;
// #endif
            LoadIntro();
        }

        /// <summary>
        /// 监听房间加载事件
        /// </summary>
        /// <param name="data"></param>
        public async void OnLoadRoomEvent(object data)
        {
            if (data is Room)
            {
                var currentRoom = data as Room;
                currentRoomVector = new(currentRoom.colume, currentRoom.line);
                _currentScene = currentRoom.roomData.sceneToLoad;

            }
            //异步

            await UnloadCurrentSceneTask();
            await LoadSceneTask();
            afterRoomLoadEvent.RaiseEvent(currentRoomVector,this);
            
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
        
        /// <summary>
        /// 监听菜单加载事件
        /// </summary>
        public async void LoadMenu()
        {
            await UnloadCurrentSceneTask();
            _currentScene = menu;
            await LoadSceneTask();
            UIManager.Instance.OnMenuLoaded();
        }
        
        /// <summary>
        /// 加载 过场动画
        /// </summary>
        private async void LoadIntro()
        {
            _currentScene = intro;
            await LoadSceneTask();
        }
        
    }
}

