using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum UIDirection
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 4,
    Right = 8,
    UpLeft = Up | Left,
    UpRight = Up | Right,
    DownLeft = Down | Left,
    DownRight = Down | Right,
}

public enum ButtonType
{
    None = 0,
    NorthButton = 1,
    SouthButton = 2,
    EastButton = 4,
    WestButton = 8,
    NSButton = NorthButton | SouthButton,
    NEButton = NorthButton | EastButton,
    NWButton = NorthButton | WestButton,
    SEButton = SouthButton | EastButton,
    SWButton = SouthButton | WestButton,
    EWButton = EastButton | WestButton,
    NSEButton = NorthButton | SouthButton | EastButton,
    NSWButton = NorthButton | SouthButton | WestButton,
    SEWButton = SouthButton | EastButton | WestButton,
    NSEWButton = NorthButton | SouthButton | EastButton | WestButton,
}

public class InputBehavior 
{
    private IInputHandler handler;

    private float startDirInput = 0;
    private bool dirInputFlag = false;
    UIDirection consecutiveDir = default;

    public InputBehavior(IInputHandler handler)
    {
        this.handler = handler;
    }

    /// <summary>
    /// 単発入力
    /// </summary>
    /// <param name="info"></param>
    public void InputButton(InputInfo info)
    {
        ButtonType res = ButtonType.None;

        if (info.Controller.EastButton.IsTrigger()) res |= ButtonType.EastButton;
        if (info.Controller.SouthButton.IsTrigger()) res |= ButtonType.SouthButton;

        if (res != ButtonType.None)
        {
            handler.InputButtonEvent(info, res);
        }
    }

    /// <summary>
    /// 連続入力
    /// </summary>
    /// <param name="info"></param>
    public void InputConsecutiveButton(InputInfo info)
    {
        ButtonType res = ButtonType.None;

        if (info.Controller.EastButton.IsHold()) res |= ButtonType.EastButton;
        if (info.Controller.SouthButton.IsHold()) res |= ButtonType.SouthButton;

        if (res != ButtonType.None)
        {
            handler.InputButtonEvent(info, res);
        }
    }

    /// <summary>
    /// 方向単発入力
    /// </summary>
    /// <param name="info"></param>
    public void InputDirection(InputInfo info)
    {
        Vector2 val = info.Controller.LeftStick.Value;

        UIDirection dir = ToDirection(val);

        if (dir != UIDirection.None)
        {
            if (consecutiveDir != UIDirection.None) return;
            handler.InputDirectionEvent(info, dir);

            // 入力内容記録
            consecutiveDir = dir;
            // 入力時間測定
            startDirInput = Time.realtimeSinceStartup;
        }
        else
        {
            consecutiveDir = dir;
        }
    }

    /// <summary>
    /// 方向連続入力
    /// </summary>
    /// <param name="info"></param>
    public void InputConsecutiveDirection(InputInfo info)
    {
        Vector2 val = info.Controller.LeftStick.Value;

        UIDirection dir = ToDirection(val);

        if (consecutiveDir == dir && dir != UIDirection.None)
        {
            // 入力内容が同じだった場合、入力開始時間から現在時間を引いてホールド時間を計測し、比較する
            if (InputSystem.settings.defaultHoldTime < Time.realtimeSinceStartup - startDirInput)
            {
                handler.InputDirectionEvent(info, dir);
            }
        }
        else
        {
            consecutiveDir = UIDirection.None;
            startDirInput = 0;
        }
    }

    UIDirection ToDirection(Vector2 value)
    {
        UIDirection res = UIDirection.None;

        if (value.x < 0) res |= UIDirection.Left;
        else if (value.x > 0) res |= UIDirection.Right;
        
        if (value.y < 0) res |= UIDirection.Down;
        else if (value.y > 0) res |= UIDirection.Up;

        return res;
    }

}
