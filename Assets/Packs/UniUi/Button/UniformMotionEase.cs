﻿using System;
using UniRx;
using UnityEngine;
using Worldreaver.UniTween;
using IScheduler = Worldreaver.UniTween.IScheduler;

namespace Worldreaver.UniUI
{
    /// <summary>
    /// only motion when OnPointerDown call
    /// no motion when OnPointerUp or OnPointerExit
    /// </summary>
    [Serializable]
    public class UniformMotionEase : IMotion
    {
#pragma warning disable 0649
        public Vector3 percentScaleDown = new Vector3(1.15f, 1.15f, 1f);
        public float durationDown = 0.1f;
        public Easing.Type easeDown = Easing.Type.OutQuad;
        public float durationUp = 0.1f;
        public Easing.Type easeUp = Easing.Type.OutQuad;
#pragma warning restore 0649

        public Vector3 PercentScaleDown => percentScaleDown;
        public IScheduler UnuseScaleScheduler => _scheduler ?? (_scheduler = new UnscaledTimeScheduler());

        private IScheduler _scheduler;
        private IDisposable _disposableDown;
        private IDisposable _disposableUp;

        public void MotionUp(
            Vector3 defaultScale,
            RectTransform affectObject)
        {
        }

        public void MotionDown(
            Vector3 defaultScale,
            RectTransform affectObject)
        {
            DisposeUp();
            var tween = Easing.Interpolate(easeDown, durationDown);
            DisposeDown();
            _disposableDown = Tweener.Play(affectObject.localScale,
                    new Vector3(defaultScale.x * PercentScaleDown.x, defaultScale.y * PercentScaleDown.y),
                    tween,
                    UnuseScaleScheduler)
                .DoOnCompleted(() =>
                {
                    var tweenUp = Easing.Interpolate(easeUp, durationUp);
                    DisposeUp();
                    _disposableUp = Tweener.Play(affectObject.localScale, defaultScale, tweenUp, UnuseScaleScheduler)
                        .SubscribeToLocalScale(affectObject)
                        .AddTo(affectObject);
                })
                .SubscribeToLocalScale(affectObject)
                .AddTo(affectObject);
        }

        public void DisposeDown()
        {
            _disposableDown?.Dispose();
            _disposableDown = null;
        }

        public void DisposeUp()
        {
            _disposableUp?.Dispose();
            _disposableUp = null;
        }
    }
}