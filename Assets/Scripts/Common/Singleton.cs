using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒVƒ“ƒOƒ‹ƒgƒ“
/// </summary>
/// <typeparam name="TClass"></typeparam>
public class Singleton<TClass> where TClass : class, new()
{
    static private TClass instance;

    static public TClass Instance
    {
        get {

            if (instance == null)
            {
                instance = new TClass();
            }

            return instance;
        }
    }

    ~Singleton()
    {
        Dispose();
    }

    static public void Dispose()
    {
        instance = default;
    }
}
