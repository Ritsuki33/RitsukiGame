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
    public InputContext<float> Decide => this[Button.EastButton];
    public InputContext<float> Back => this[Button.EastButton];

    // Axis
    public InputContext<Vector2> Move => this[Axis.LeftStick];

    private InputContext<float>[] inputButtonContext = new InputContext<float>[(int)Button.Max];
    private InputContext<Vector2>[] inputAxisContext = new InputContext<Vector2>[(int)Axis.Max];

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="actions"></param>
    public InputController(PlayerInputAction.UIActions actions)
    {
        // ボタン設定
        this[Button.EastButton] = new InputContext<float>(actions.Decide);
        this[Button.SouthButton] = new InputContext<float>(actions.Back);

        // Axis
        this[Axis.LeftStick] = new InputContext<Vector2>(actions.Move);
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
    /// インデクサ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public InputContext<float> this[Button type]
    {
        get
        {
            return inputButtonContext[(int)type];
        }
        set
        {
            inputButtonContext[(int)type] = value;
        }
    }
    
    /// <summary>
    /// インデクサ
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public InputContext<Vector2> this[Axis type]
    {
        get { 
            return inputAxisContext[(int)type];
        }
        set
        {
            inputAxisContext[(int)type] = value;
        }
    }
}
