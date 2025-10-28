using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolTool : MonoBehaviour
{
    public GameObject objectPre;//预制体
    private ObjectPool<GameObject> _pool;

    private void Start()
    {
        ///对象池的建立
        _pool = new(
            createFunc: () => Instantiate(objectPre, transform),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        PreFiilPool(7);
    }

    private void PreFiilPool(int count)
    {
        List<GameObject> _preList = new(count);
        for (int i = 0; i < count; i++)
        {
            var obj = _pool.Get();
            _preList.Add(obj);
        }

        foreach (var obj in _preList)
        {
            _pool.Release(obj);
        }
    }

    /// <summary>
    /// 取出池中的一个物体
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        return _pool.Get();
    }

    /// <summary>
    /// 归还一个物体
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnObjectToPool(GameObject obj)
    {
        _pool.Release(obj);
    }
    
}