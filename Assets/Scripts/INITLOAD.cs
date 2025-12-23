using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class INITLOAD : MonoBehaviour
{
    public AssetReference load;

    private void Awake()
    {
        Addressables.LoadSceneAsync(load);
    }
}
