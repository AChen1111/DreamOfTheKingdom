using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour {
    public PoolTool pool;
    public List<CardDataSO> cardDataList; //全部卡牌

    void Start()
    {
        InitCardList();
    }
    private void InitCardList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new(handle.Result);
        }
        else
        {
            Debug.LogWarning("加载失败");
        }
    }
}