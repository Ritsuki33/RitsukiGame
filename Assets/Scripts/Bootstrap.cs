using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {

#if UNITY_EDITOR
        Debug.Log("ProjectManager loading...");
#endif
        ResourceManager.Instance.LoadAssetAsync<GameObject>(
            "ProjectManager",
            (obj) =>
            {
                DontDestroyOnLoad(obj);
#if UNITY_EDITOR
                Debug.Log("ProjectManager load successed");
#endif
            },
            () =>
            {
#if UNITY_EDITOR
                Debug.Log("ProjectManager load failed");
#endif
            });
    }
}
