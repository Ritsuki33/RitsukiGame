﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IScreen
{
    public IPresenter Initialize();
}

public abstract class ScreenBase<TView, TPresenter, TViewModel> : MonoBehaviour, IScreen
    where TView : ScreenBase<TView, TPresenter, TViewModel>
    where TPresenter : ScreenPresenterBase<TView, TPresenter, TViewModel>, new()
    where TViewModel : ScreenViewModelBase, new()
{
    public abstract void Configure(TViewModel viewModel);

    IPresenter presenter;
    IPresenter IScreen.Initialize()
    {
        var presenter = new TPresenter();

        presenter.Initialize((TView)this);

        return presenter;
    }

}
