using System;
using Amigyo.Input;
using UniRx;
using UnityEngine;

namespace Amigyo.Ship
{
    public class Shooter : MonoBehaviour
    {
        //  Zenjectにfield-injectionさせる。
        private IInputProvider inputProvider = new MouseInputProvider();

        [SerializeField]
        private GameObject netPrefab;

        private void Start()
        {
            //  角度が変化したときの処理。
            this.inputProvider.Angle.Subscribe(this.OnAngleChanged).AddTo(this);

            //  ボタンが押されたときの処理。
            this.inputProvider.IsPressed.Where(isPressed => isPressed).Subscribe(_ => this.OnButtonPressed()).AddTo(this);

            //  コントローラの値をリッスンする。
            this.inputProvider.Listen();

            Disposable.Create(() => this.inputProvider?.Dispose()).AddTo(this);
        }

        //  角度が変化したときの処理。
        private void OnAngleChanged(float angle)
        {
            var unityAngle = 90 - angle;
            this.transform.rotation = Quaternion.AngleAxis(unityAngle, new Vector3(0, 1, 0));
        }

        //  ボタンが押されたときの処理。
        private void OnButtonPressed()
        {
            Instantiate(this.netPrefab, this.transform.position, this.transform.rotation);
        }
    }
}