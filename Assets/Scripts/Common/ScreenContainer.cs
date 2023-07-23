using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenContainer<TEnum> where TEnum : Enum
{

    Dictionary<TEnum, IScreen> screenDic = new Dictionary<TEnum, IScreen>();

    //TEnum currentType = default;
    IScreen currentScreen = default;
    IPresenter m_ActivePresenter = default;

    /// <summary>
    /// スクリーンの登録
    /// </summary>
    /// <param name="type"></param>
    /// <param name="screen"></param>
    public void Register(TEnum type, IScreen screen)
    {
        screenDic[type] = screen;
    }

    /// <summary>
    /// スクリーンの削除
    /// </summary>
    /// <param name="type"></param>
    public void UnRegister(TEnum type)
    {
        screenDic.Remove(type);
    }

    public void OnUpdate(InputInfo info)
    {
        m_ActivePresenter?.OnUpdate(info);
    }

    /// <summary>
    /// スクリーン遷移
    /// </summary>
    /// <param name="type"></param>
    /// <param name="immediate"></param>
    public void TransitTo(TEnum type, bool immediate = true)
    {
        if (!screenDic.ContainsKey(type))
        {
            Debug.LogError($"{type.ToString()}にスクリーンが登録されていません。");
            return;
        }
        if (currentScreen == screenDic[type])
        {
            Debug.Log($"{type.ToString()}遷移先が現在と同じなので遷移しません。");
            return;
        }

        TransitProcess(type, immediate);
    }


    void TransitProcess(TEnum type, bool immediate)
    {
        if (immediate)
        {
            if (m_ActivePresenter != null)
            {
                Hide();
            }

            // シーンの初期化
            m_ActivePresenter = screenDic[type].Initialize();

            // 新しいシーンに合わせる
            currentScreen = screenDic[type];

            Show();
        }
        else
        {
            RunCroutineManager.Instance.StartCroutine(TransitCroutine(type));
        }
    }

    private void Hide()
    {
        m_ActivePresenter.Hide();
        m_ActivePresenter.HideComplete();
    }

    private void Show()
    {
        m_ActivePresenter.Show();
        m_ActivePresenter.ShowComplete();
    }

    private IEnumerator TransitCroutine(TEnum type)
    {
        if (m_ActivePresenter != null)
        {
            yield return HideCoroutine(m_ActivePresenter);
        }

        // シーンの初期化
        m_ActivePresenter = screenDic[type].Initialize();

        // 新しいシーンに合わせる
        currentScreen = screenDic[type];

        yield return ShowCoroutine(m_ActivePresenter);
    }

    private IEnumerator HideCoroutine(IPresenter presenter)
    {
        yield return presenter.HideCoroutine();
        presenter.HideComplete();

    }

    private IEnumerator ShowCoroutine(IPresenter presenter)
    {
        yield return presenter.ShowCoroutine();
        m_ActivePresenter.ShowComplete();

    }
}