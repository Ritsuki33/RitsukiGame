using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputContext<T> where T : struct
{
    public bool isUse { get; set; }

    protected T value;

    protected InputAction inputAction = default;

    public InputContext(InputAction inputAction)
    {
        isUse = true;
        this.inputAction = inputAction;
    }

    public void InputUpdate()
    {
        value = inputAction.ReadValue<T>();
    }

    public void SetUsable(bool val)
    {
        isUse = val;
    }
}
