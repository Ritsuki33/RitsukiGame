using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPresenter
{
    public void Show();
    public void ShowComplete();
    public IEnumerator ShowCoroutine();
    public void Hide();
    public void HideComplete();
    public IEnumerator HideCoroutine();
}

public abstract class ScreenPresenterBase<TView, TPresenter, TViewModel>: IPresenter
    where TView : ScreenBase<TView, TPresenter, TViewModel>
    where TPresenter : ScreenPresenterBase<TView, TPresenter, TViewModel>, new()
    where TViewModel : ScreenViewModelBase, new()
{
    public abstract void Configure(TView screen, TViewModel viewModel);

    private TView m_Screen = default;
    private TViewModel m_ViewModel = default;

    /// <summary>
    /// プレゼンター側でMVPの構築を行う
    /// </summary>
    /// <param name="screen"></param>
    public void Initialize(TView screen)
    {
        m_Screen = screen;
        m_ViewModel=new TViewModel();

        // Model構築
        m_ViewModel.Configure();

        // プレゼンター構築
        Configure(m_Screen, m_ViewModel);

        // ビューの構築
        m_Screen.Configure(m_ViewModel);
    }

    public void Hide()
    {
    }

    public void HideComplete()
    {
    }

    public IEnumerator HideCoroutine()
    {
        yield return null;
    }



    public void Show()
    {
    }

    public void ShowComplete()
    {
    }

    public IEnumerator ShowCoroutine()
    {
        yield return null;
    }
}
