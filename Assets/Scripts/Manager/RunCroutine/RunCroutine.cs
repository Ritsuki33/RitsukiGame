using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCroutine : MonoBehaviour
{
    public void Run(IEnumerator croutine)
    {
        StartCoroutine(croutine);
    }
}
