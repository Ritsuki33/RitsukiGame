using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// シングルトンコンポーネント
/// </summary>
/// <typeparam name="TClass"></typeparam>
public abstract class SingletonComponent<TClass> : MonoBehaviour where TClass : MonoBehaviour
{
    static private TClass instance;

    static public TClass Instance => instance;

    private void Awake()
    {
        Assert.IsNull(instance, $"{this.GetType().Name}はシングルトンコンポーネントのため、単一でなければなりません");

        instance = this as TClass;

        OnAwake();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    static public void Dispose()
    {
        instance = default;
    }

    protected virtual void OnAwake() { }
}