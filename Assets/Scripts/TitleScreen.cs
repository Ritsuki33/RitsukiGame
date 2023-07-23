using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TitleScreen : ScreenBase<TitleScreen, TitleScreenPresenter, TitleScreenViewModel>
{
    public override void Configure(TitleScreenViewModel viewModel)
    {

    }
}

public class TitleScreenPresenter : ScreenPresenterBase<TitleScreen, TitleScreenPresenter, TitleScreenViewModel>
{
    public override void Configure(TitleScreen screen, TitleScreenViewModel viewModel)
    {

    }

    public override void InputButtonEvent(IConsumable input, ButtonType btn)
    {
        Debug.Log(btn);
    }

    public override void InputDirectionEvent(IConsumable input, UIDirection direction)
    {
        Debug.Log(direction);
    }
}

public class TitleScreenViewModel : ScreenViewModelBase
{
    public override void Configure()
    {

    }
}