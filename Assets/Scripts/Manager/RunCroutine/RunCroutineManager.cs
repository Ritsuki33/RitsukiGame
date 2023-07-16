using System.Collections;
using UnityEngine;

public class RunCroutineManager : Singleton<RunCroutineManager>
{
    GameObject gameObject = new GameObject();
    RunCroutine runCroutine = default;
    public  RunCroutineManager()
    {
        runCroutine = gameObject.AddComponent<RunCroutine>();
    }

    public void StartCroutine(IEnumerator croutine)
    {
        runCroutine.Run(croutine);
    }
}
