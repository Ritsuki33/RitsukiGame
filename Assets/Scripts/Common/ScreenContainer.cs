using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenContainer<TEnum> where TEnum : Enum
{

    Dictionary<TEnum, IScreen> screenDic = new Dictionary<TEnum, IScreen>();

    IScreen currentScreen = default;
    IPresenter currentPresenter = default;

    /// <summary>
    /// スクリーンの登録
    /// </summary>
    /// <param name="type"></param>
    /// <param name="screen"></param>
    public void Register(TEnum type,IScreen screen)
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

        var nextScreen = screenDic[type];
        if (currentScreen == nextScreen)
        {
            Debug.Log($"{type.ToString()}遷移先が現在と同じなので遷移しません。");
            return;
        }
        TransitProcess(nextScreen, immediate);
    }


    void TransitProcess(IScreen nextScreen, bool immediate)
    {
        if (immediate)
        {
            currentPresenter?.Hide();
            currentPresenter?.HideComplete();


            // シーンの初期化
            currentPresenter = currentScreen.Initialize();

            // 新しいシーンに合わせる
            currentScreen = nextScreen;

            currentPresenter.Show();
            currentPresenter.ShowComplete();
        }
        else
        {
            //未実装
        }
    }

    void Hide(IPresenter presenter)
    {
        presenter.Hide();
        currentPresenter.HideComplete();
    }
}
