using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    /// <summary>
    /// ボタン入力イベント
    /// </summary>
    /// <param name="input"></param>
    /// <param name="direction"></param>
    public void InputDirectionEvent(IConsumable input,UIDirection direction);
    
    /// <summary>
    /// 方向入力イベント
    /// </summary>
    /// <param name="input"></param>
    /// <param name="btn"></param>
    public void InputButtonEvent(IConsumable input, ButtonType btn);

}

public interface IPresenter
{
    public void OnUpdate(InputInfo info);

    public void Show();
    public void ShowComplete();
    public IEnumerator ShowCoroutine();
    public void Hide();
    public void HideComplete();
    public IEnumerator HideCoroutine();
}

public abstract class ScreenPresenterBase<TView, TPresenter, TViewModel>: IPresenter,IInputHandler
    where TView : ScreenBase<TView, TPresenter, TViewModel>
    where TPresenter : ScreenPresenterBase<TView, TPresenter, TViewModel>, new()
    where TViewModel : ScreenViewModelBase, new()
{
    public abstract void Configure(TView screen, TViewModel viewModel);

    private TView m_Screen = default;
    private TViewModel m_ViewModel = default;

    InputBehavior inputBehavior = default;

    /// <summary>
    /// プレゼンター側でMVPの構築を行う
    /// </summary>
    /// <param name="screen"></param>
    public void Initialize(TView screen)
    {
        inputBehavior = new InputBehavior(this);

        m_Screen = screen;
        m_ViewModel=new TViewModel();

        // Model構築
        m_ViewModel.Configure();

        // プレゼンター構築
        Configure(m_Screen, m_ViewModel);

        // ビューの構築
        m_Screen.Configure(m_ViewModel);
    }

    public void OnUpdate(InputInfo info)
    {
        if (info.Controller == null) return;

        // ボタン入力
        inputBehavior.InputButton(info);
        inputBehavior.InputConsecutiveButton(info);


        // 方向入力
        inputBehavior.InputDirection(info);
        inputBehavior.InputConsecutiveDirection(info);
    }

    public void Hide()
    {
        m_Screen.OnHide();
        OnHide();
        m_Screen.gameObject.SetActive(false);
    }

    public void HideComplete()
    {
        m_Screen.OnHideComplete();
        OnHideComplete();
    }

    public IEnumerator HideCoroutine()
    {
        yield return m_Screen.OnHideCoroutine();
        yield return OnHideCoroutine();
        m_Screen.gameObject.SetActive(false);

    }

    public void Show()
    {
        m_Screen.OnShow();
        OnShow();
        m_Screen.gameObject.SetActive(true);
    }

    public void ShowComplete()
    {
        m_Screen.OnShowComplete();
        OnShowComplete();
    }

    public IEnumerator ShowCoroutine()
    {
        yield return m_Screen.OnShowCoroutine();
        yield return OnShowCoroutine();

        m_Screen.gameObject.SetActive(true);

    }

    protected virtual void OnShow() { }
    protected virtual IEnumerator OnShowCoroutine() { yield return null; }
    protected virtual void OnShowComplete() { }

    protected virtual void OnHide() { }
    protected virtual IEnumerator OnHideCoroutine() { yield return null; }
    protected virtual void OnHideComplete() { }

    public virtual void InputDirectionEvent(IConsumable input, UIDirection direction) { }
    public virtual void InputButtonEvent(IConsumable input, ButtonType btn) { }
}
