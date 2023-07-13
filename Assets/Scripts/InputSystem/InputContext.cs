using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputContext<T> where T : struct
{
    T value;

    public T Value => value;

    InputAction inputAction = default;

    public InputContext(InputAction inputAction)
    {
        this.inputAction = inputAction;
    }

    public void InputUpdate()
    {
        value = inputAction.ReadValue<T>();
    }
}
