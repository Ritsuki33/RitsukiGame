using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonComponent<InputManager>
{
    PlayerInputAction inputAction = default;
    InputController inputController = default;

    private void Start()
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
}
