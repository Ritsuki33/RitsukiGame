using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ステートマシン
/// </summary>
/// <typeparam name="T"></typeparam>
public class StateMachine<T> where T : Enum
{

    private Dictionary<T, StateNode<T>> stateNodes = new Dictionary<T, StateNode<T>>();


    bool init = true;
    private T currentState = default;
    private T preState = default;
    private T nextState = default;
    
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="stateType"></param>
    public void InitializeTo(T stateType)
    {
        currentState = stateType;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void OnUpdate()
    {
        if (init || !preState.Equals(currentState))
        {
            // 入場処理
            preState = currentState;
            stateNodes[currentState].onEnter?.Invoke();

            init = false;
        }

        // 更新処理
        nextState = stateNodes[currentState].onUpdate.Invoke();

        if (!nextState.Equals(currentState))
        {
            // 終了処理
            stateNodes[currentState].onExit?.Invoke();
            currentState = nextState;
        }
    }

    /// <summary>
    /// ステートノードの登録
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="onEnter"></param>
    /// <param name="onUpdate"></param>
    /// <param name="onExit"></param>
    public void RegisterStateNode(T stateType, Func<T> onEnter, Func<T> onUpdate, Func<T> onExit)
    {

        if (stateNodes.ContainsKey(stateType))
        {
            // 更新
            stateNodes[stateType].onEnter = onEnter;
            stateNodes[stateType].onUpdate = onUpdate;
            stateNodes[stateType].onExit = onExit;
        }
        else
        {
            // 追加
            StateNode<T> node = new StateNode<T>();
            node.onEnter = onEnter;
            node.onUpdate = onUpdate;
            node.onExit = onExit;

            stateNodes.Add(stateType, node);
        }
    }
       
    /// <summary>
    /// ノードの削除
    /// </summary>
    /// <param name="stateType"></param>
    public void DeleteStateNode(T stateType)
    {
        if (stateNodes.ContainsKey(stateType)) stateNodes.Remove(stateType);
    }

   
}
