using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public enum Button
    {
        NorthButton,
        EastButton,
        SouthButton,
        WestButton,
        Max,
    }

    public enum Axis
    {
        LeftStick,
        Max,
    }

    //　ボタン
    public IInputContextButton EastButton => this[Button.EastButton];
    public IInputContextButton SouthButton => this[Button.SouthButton];

    // Axis
    public IInputContextDirection LeftStick => this[Axis.LeftStick];

    private InputContextButton[] inputButtonContext = new InputContextButton[(int)Button.Max];
    private InputContextDirection[] inputAxisContext = new InputContextDirection[(int)Axis.Max];

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="actions"></param>
    public InputController(PlayerInputAction.UIActions actions)
    {
        // ボタン設定
        this[Button.EastButton] = new InputContextButton(actions.Decide);
        this[Button.SouthButton] = new InputContextButton(actions.Back);

        // Axis
        this[Axis.LeftStick] = new InputContextDirection(actions.Move);
    }

    /// <summary>
    /// 入力更新
    /// </summary>
    public void InputUpdate()
    {
        foreach (var i in inputButtonContext)
        {
            i?.InputUpdate();
        }

        foreach (var i in inputAxisContext)
        {
            i?.InputUpdate();
        }
    }

    /// <summary>
    /// ボタン取得
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public IInputContextButton GetContextButton(Button button)
    {
        return this[button];
    }

    /// <summary>
    /// Axis取得
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    public IInputContextDirection GetContextDirection(Axis axis)
    {
        return this[axis];
    }

    /// <summary>
    /// インデクサ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IInputContextButton this[Button type]
    {
        get
        {
            return inputButtonContext[(int)type];
        }
        set
        {
            inputButtonContext[(int)type] = value as InputContextButton;
        }
    }
    
    /// <summary>
    /// インデクサ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IInputContextDirection this[Axis type]
    {
        get
        {
            return inputAxisContext[(int)type];
        }
        set
        {
            inputAxisContext[(int)type] = value as InputContextDirection;
        }
    }
}
