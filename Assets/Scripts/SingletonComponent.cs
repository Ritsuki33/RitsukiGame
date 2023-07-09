using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// シングルトンコンポーネント
/// </summary>
/// <typeparam name="TClass"></typeparam>
public class SingletonComponent<TClass> : MonoBehaviour where TClass : MonoBehaviour
{
    static private TClass instance;

    static public TClass Instance => instance;

    private void Awake()
    {
        Assert.IsNull(instance, $"{this.GetType().Name}はシングルトンコンポーネントのため、単一でなければなりません");

        instance = this as TClass;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    static public void Dispose()
    {
        instance = default;
    }
}