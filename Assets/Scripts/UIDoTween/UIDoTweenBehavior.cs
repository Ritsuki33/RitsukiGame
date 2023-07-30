using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;
using UnityEngine.UI;

/*
===================================================================================
DoTweenのChangeStartValueに関して

    Tweenの開始時に初期値を設定するメソッド。Sequeanceを使った場合も同様。
    これで、あるTweenが始まったときは、初期値から開始できればと思っていたが、
    同じTweenでなければ、開始時に初期値が反映されず、Tweenが異なるとシーケンス開始時に初期化が走っていまう.
    不具合なのか、仕様なのかは不明.
    ChangeStartValueに関する記事も少ないため、このプロジェクトでは非推奨とする。
===================================================================================
 */
public class UIDoTweenBehavior : MonoBehaviour
{
    public enum LinkType
    {
        Append,
        Join
    }

    //[SerializeField] UIDoTweenParam[] hide = default;

    [Serializable]
    class UIDoTweenParam
    {
        [SerializeField] public LinkType linkType = default;
        [SerializeField] public UIDoTween tween = default;
        [SerializeField] public float delay = default;
    }

    [SerializeField] List<UIDoTweenParam> show = default;
    [SerializeField] List<UIDoTweenParam> hide = default;

    public RectTransform halfwayTransform = default;

    private void Awake()
    {
        halfwayTransform = this.transform as RectTransform;
    }

    private void OnValidate()
    {
        foreach (var i in show)
        {
            i.tween.OnValidate();
        }
    }

    public void Show(Action isCompleted)
    {
        Sequence sequence = DOTween.Sequence();

        foreach (var i in show)
        {
            switch (i.linkType)
            {
                case LinkType.Append:
                    sequence.Append(i.tween.CreateTween(this).SetDelay(i.delay));
                    break;
                case LinkType.Join:
                    sequence.Join(i.tween.CreateTween(this).SetDelay(i.delay));
                    break;
            }
        }

        sequence.OnComplete(() => { isCompleted?.Invoke(); });

        sequence.Play();
    }

    public void Hide(Action isCompleted)
    {
        Sequence sequence = DOTween.Sequence();

        foreach (var i in show)
        {
            switch (i.linkType)
            {
                case LinkType.Append:
                    sequence.Append(i.tween.CreateTween(this).SetDelay(i.delay));
                    break;
                case LinkType.Join:
                    sequence.Join(i.tween.CreateTween(this).SetDelay(i.delay));
                    break;
            }
        }
    }
}


[Serializable]
public class UIDoTween
{
    public enum TweenType
    {
        Move,
        JumpAnchor,
        Fade,
        Rotate,
        Scale,
    }

    [SerializeField] public TweenType type = default;
    [SerializeReference] private TweenParam tweenParam = default;

    public void OnValidate()
    {

        switch (type)
        {
            case TweenType.Move:
                if (!(tweenParam is Move)) tweenParam = new Move();
                break;
            case TweenType.JumpAnchor:
                if (!(tweenParam is JumpAnchor)) tweenParam = new JumpAnchor();
                break;
            case TweenType.Fade:
                if (!(tweenParam is Fade)) tweenParam = new Fade();
                break;
            case TweenType.Rotate:
                if (!(tweenParam is Rotate)) tweenParam = new Rotate();
                break;
            case TweenType.Scale:
                if (!(tweenParam is Scale)) tweenParam = new Scale();
                break;
        }
    }

    public Tween CreateTween(UIDoTweenBehavior obj)
    {
        Assert.IsNotNull(tweenParam, "UIDoTweenパラメータがNULLです。");
        return tweenParam.CreateTween(obj);
    }

    /// <summary>
    /// Tweenパラメータ
    /// </summary>
    [Serializable]
    public class TweenParam
    {
        public virtual Tween CreateTween(UIDoTweenBehavior obj) { return null; }
    }

    [Serializable]
    class Move : TweenParam
    {
        [SerializeField, Header("開始地点")] private Vector2 startValue;
        [SerializeField, Header("終了地点")] private Vector2 endValue;
        [SerializeField, Header("間隔（秒）")] private float duration;
        [SerializeField, Header("イース")] private Ease ease = default;

        public override Tween CreateTween(UIDoTweenBehavior obj)
        {
            RectTransform rectTransform = obj.transform as RectTransform;

            Sequence seq = DOTween.Sequence();
            seq.Append(rectTransform.DOAnchorPos(startValue, 0));
            seq.Join(rectTransform.DOAnchorPos(endValue, duration).SetEase(ease));
            return seq;
        }
    }

    [Serializable]
    class Fade : TweenParam
    {
        [SerializeField, Header("開始値"), Range(0, 1)] private float startValue;
        [SerializeField, Header("最終値"), Range(0, 1)] private float endValue;
        [SerializeField, Header("間隔（秒）")] private float duration;
        [SerializeField, Header("イース")] private Ease ease = default;

        public override Tween CreateTween(UIDoTweenBehavior obj)
        {
            var image = obj.GetComponent<Image>();
            
            Sequence seq = DOTween.Sequence();
            seq.Append(image?.material.DOFade(startValue, 0));
            seq.Join(image?.material.DOFade(endValue, duration).SetEase(ease));
            return seq;
        }
    }

    [Serializable]
    class Rotate : TweenParam
    {
        [SerializeField, Header("開始角度")] private Vector3 startRotate;
        [SerializeField, Header("最終角度")] private Vector3 endRotate;
        [SerializeField, Header("間隔（秒）")] private float duration;
        [SerializeField, Header("回転方法")] private RotateMode mode = RotateMode.FastBeyond360;
        [SerializeField, Header("イース")] private Ease ease = default;

        public override Tween CreateTween(UIDoTweenBehavior obj)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(obj.transform.DORotate(startRotate, duration, mode));
            seq.Append(obj.transform.DORotate(endRotate, duration, mode).SetEase(ease));
            return seq;
        }
    }

  

    [Serializable]
    class JumpAnchor : TweenParam
    {
        [SerializeField, Header("開始地点")] private Vector2 startValue;
        [SerializeField, Header("終了地点")] private Vector2 endValue;
        [SerializeField, Header("ジャンプ力")] private float jumpPower;
        [SerializeField, Header("ジャンプ回数")] private int jumpNum;
        [SerializeField, Header("間隔（秒）")] private float duration;
        [SerializeField, Header("イース")] private Ease ease = default;

        private Vector2 goal = default;

        public override Tween CreateTween(UIDoTweenBehavior obj)
        {
            Sequence seq = DOTween.Sequence();
            RectTransform rectTransform = obj.transform as RectTransform;
            seq.Append(rectTransform.DOAnchorPos(startValue, 0));
            seq.Join(rectTransform.DOJumpAnchorPos(endValue, jumpPower, jumpNum, duration).SetEase(ease));
            return seq;
        }
    }

    [Serializable]
    class Scale : TweenParam
    {
        [SerializeField, Header("開始スケール")] private Vector3 startValue;
        [SerializeField, Header("終了スケール")] private Vector3 endValue;
        [SerializeField, Header("間隔（秒）")] private float duration;
        [SerializeField, Header("イース")] private Ease ease = default;


        public override Tween CreateTween(UIDoTweenBehavior obj)
        {
            Sequence seq = DOTween.Sequence();
            
            RectTransform rectTransform = obj.transform as RectTransform;
            seq.Append(rectTransform.DOScale(startValue, 0));
            seq.Join(rectTransform.DOScale(endValue, duration).SetEase(ease));
            return seq;
        }
    }
}
