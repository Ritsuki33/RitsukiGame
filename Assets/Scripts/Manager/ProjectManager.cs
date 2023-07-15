using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : SingletonComponent<ProjectManager>
{
    public static bool IsActive
    {
        get
        {
            return Instance != null && Instance.isActive;
        }
    }

    /// <summary>
    /// アクティブかどうか
    /// </summary>
    private bool isActive = true;


    /// <summary>
    /// アクティブの切り替え
    /// </summary>
    /// <param name="flag"></param>
    public void SetActive(bool flag)
    {
        isActive = flag;
    }
}
