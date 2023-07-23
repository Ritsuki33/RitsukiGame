using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputContextButton
{
    public bool IsTrigger();

    public bool IsPressed();

    public bool IsHold();
    public bool IsHold(float time);

}


public class InputContextButton : InputContext<float>, IInputContextButton
{
    float StartTime = 0;

    public InputContextButton(InputAction inputAction) : base(inputAction)
    {
        // 入力が発生した瞬間測定開始
        this.inputAction.started += (obj) => { StartTime = Time.realtimeSinceStartup; };
    }

    /// <summary>
    /// 入力のトリガー
    /// </summary>
    /// <returns></returns>
    public bool IsTrigger()
    {

        return (isUse) ? inputAction.WasPressedThisFrame() : false;
    }

    /// <summary>
    /// 押しっぱなし（即時）
    /// </summary>
    /// <returns></returns>
    public bool IsPressed()
    {
        return inputAction.IsPressed();
    }

    /// <summary>
    /// 押しっぱなし (設定ファイルの初期ホールド時間)
    /// </summary>
    /// <returns></returns>
    public bool IsHold()
    {
        return (isUse) ? IsHold(InputSystem.settings.defaultHoldTime) : false;
    }

    /// <summary>
    /// 何秒後に押しっぱなし判定にするか
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool IsHold(float time)
    {
        /*
         押していなければ何もしない。
         <参考>
             InputActionPhase.Waiting => "何もしていない"
             InputActionPhase.Started => "開始"
             InputActionPhase.Performed => "閾値を超えた入力"
             InputActionPhase.Canceled => "離した"
             InputActionPhase.Disabled => "無効中"
        */
        if (inputAction.phase != InputActionPhase.Started && inputAction.phase != InputActionPhase.Performed) return false;

        return (Time.realtimeSinceStartup - StartTime) >= time;
    }
}
