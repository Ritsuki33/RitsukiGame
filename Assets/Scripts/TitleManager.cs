using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : ManagerBase
{

    enum UI
    {
        Title,
    }
    [SerializeField] TitleScreen titleScreen = default;

    private ScreenContainer<UI> screenContainer = new ScreenContainer<UI>();

    protected override void OnAwake()
    {
        screenContainer.Register(UI.Title, titleScreen);

        screenContainer.TransitTo(UI.Title);
    }
    protected override void OnUpdate()
    {
        InputInfo info = new InputInfo(InputManager.Instance.GetInput());
        screenContainer.OnUpdate(info);
    }
}
