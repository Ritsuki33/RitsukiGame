using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase : SingletonComponent<ManagerBase>
{

    private void Update()
    {
        if (!ProjectManager.Instance) return;

        OnUpdate();
    }

    protected virtual void OnUpdate() { }

}
