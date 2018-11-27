using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Amigyo.Input
{
    public sealed class MouseInputProvider : IInputProvider
    {
        private readonly BooleanDisposable booleanDisposable = new BooleanDisposable();

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private readonly Subject<float> angleSubject = new Subject<float>();

        private readonly Subject<bool> isPressedSubject = new Subject<bool>();

        public ReadOnlyReactiveProperty<float> Angle { get; private set; }

        public ReadOnlyReactiveProperty<bool> IsPressed { get; private set; }

        public MouseInputProvider()
        {
            //  プロパティの生成自体は先にやっておく。
            this.Angle = this.angleSubject.ToReadOnlyReactiveProperty().AddTo(this.disposable);
            this.IsPressed = this.isPressedSubject.ToReadOnlyReactiveProperty().AddTo(this.disposable);
            this.booleanDisposable.AddTo(this.disposable);
        }

        public bool Listen()
        {
            //  再びListenはできないようにする。
            if (this.booleanDisposable.IsDisposed) return false;

            //  Listenされたら発火用のSubjectを購読する。
            //  Listenを呼ぶ前から購読できるようにするため。
            Observable.TimerFrame(1, 1)
                .Select(_ => UnityEngine.Input.mousePosition)
                .Select(vec => vec - new Vector3(Screen.width / 2.0f, 0, 0))
                .Select(vec => Vector3.Angle(vec, Vector3.right))
                .Subscribe(angle => this.angleSubject.OnNext(angle))
                .AddTo(this.disposable);

            Observable.TimerFrame(1, 1)
                .Select(_ => UnityEngine.Input.GetMouseButtonDown(0))
                .Subscribe(flag => this.isPressedSubject.OnNext(flag))
                .AddTo(this.disposable);

            return true;
        }

        public void Dispose()
        {
            this.disposable.Dispose();
        }
    }
}