using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputContextDirection
{
    public Vector2 Value { get; }

}

public class InputContextDirection : InputContext<Vector2>, IInputContextDirection
{
    bool upFlag = false;
    public InputContextDirection(InputAction inputAction) : base(inputAction)
    {
    }

    public Vector2 Value => value;
}