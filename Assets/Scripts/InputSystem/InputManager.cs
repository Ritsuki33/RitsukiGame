using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonComponent<InputManager>
{
    PlayerInputAction inputAction = default;
    InputController inputController = default;

    protected override void OnAwake()
    {
        inputAction = new PlayerInputAction();
        Setup();

        inputController = new InputController(inputAction.UI);
    }

    private void Update()
    {
        inputController.InputUpdate();
    }

    /// <summary>
    /// インプットコントローラ取得
    /// </summary>
    /// <returns></returns>
    public InputController GetInput()
    {
        return inputController;
    }

    /// <summary>
    /// セットアップ
    /// </summary>
    void Setup()
    {
        // これをしないと入力が反映されない。
        inputAction.Enable();
    }

    /// <summary>
    /// ボタンコンテキスト取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public IInputContextButton GetContext(InputController.Button type)
    {
        return inputController[type];
    }
}
