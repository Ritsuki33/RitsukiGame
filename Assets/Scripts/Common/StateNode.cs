using System;

/// <summary>
/// ステートノード
/// </summary>
/// <typeparam name="T"></typeparam>
public class StateNode<T> where T : Enum
{
    public Func<T> onEnter;
    public Func<T> onUpdate;
    public Func<T> onExit;
}
