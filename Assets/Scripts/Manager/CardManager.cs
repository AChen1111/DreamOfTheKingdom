using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour {
    public PoolTool pool;
    public List<CardDataSO> cardDataList; //全部卡牌

    [Header("卡牌库")]
    public CardLibrarySO newGameCardLibrary; //新游戏时初始化的卡牌库
    public CardLibrarySO currentLibrary; //当前玩家开牌库

    void Awake()
    {
        InitCardList();

        //导入牌库
        foreach (var item in newGameCardLibrary.cardLibraries)
        {
            currentLibrary.cardLibraries.Add(item);
        }
    }

    void OnDisable()
    {
        currentLibrary.cardLibraries.Clear();
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

    /// <summary>
    /// 抽卡时调用获得卡牌的GameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {
        var cardObj = pool.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;
        return cardObj;
    }

    /// <summary>
    /// 回收
    /// </summary>
    /// <param name="obj"></param>
    public void DisCardObject(GameObject obj)
    {
        pool.ReturnObjectToPool(obj);
    }
}