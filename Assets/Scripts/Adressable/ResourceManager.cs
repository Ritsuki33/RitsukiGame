using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ResourceManager : Singleton<ResourceManager>
{
    /// <summary>
    /// AddressableÇ©ÇÁÉçÅ[Éh
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="successed"></param>
    /// <param name="failed"></param>
    public void LoadAssetAsync<T>(string path, Action<T> successed = null, Action failed = null) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadAssetAsync<T>(path);

        handle.Completed += op =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var instance = GameObject.Instantiate(handle.Result);
                successed?.Invoke(instance);
            }
            else
            {
                failed?.Invoke();
            }
        };
    }
}
