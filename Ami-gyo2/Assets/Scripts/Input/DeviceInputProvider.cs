using System;
using System.IO.Ports;
using UniRx;

namespace Amigyo.Input
{
    public sealed class DeviceInputProvider : IInputProvider
    {
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private readonly Subject<float> angleSubject = new Subject<float>();

        private readonly Subject<bool> isPressedSubject = new Subject<bool>();

        private bool canListen = true;

        private readonly SerialPort serialPort;

        public string PortName { get; }

        public int BaudRate { get; }

        public ReadOnlyReactiveProperty<float> Angle { get; }

        public ReadOnlyReactiveProperty<bool> IsPressed { get; }

        public DeviceInputProvider(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;

            this.serialPort = new SerialPort(portName, baudRate);
            this.disposable.Add(this.serialPort);

            //  先にプロパティの生成だけはやっておく。
            this.Angle = this.angleSubject.ToReadOnlyReactiveProperty().AddTo(this.disposable);
            this.IsPressed = this.isPressedSubject.ToReadOnlyReactiveProperty().AddTo(this.disposable);
        }

        public bool Listen()
        {
            //  再びListen()できないようにする。
            if (!this.canListen) return false;

            Observable.TimerFrame(1, 1)
                .Select(_ =>
                {
                    //  データを読み取り、残りをバッファから削除する。
                    //  データの量が多すぎて、処理が追いつかなくなるため。
                    var data = this.serialPort.ReadLine();
                    this.serialPort.DiscardInBuffer();
                    return data;
                })
                .Select(str =>
                {
                    try
                    {
                        //  bool型とint型にパースしておく。
                        var strs = str.Split(',');
                        var isPressed = strs[0].Trim() == "1" ? true : false;
                        var angleRawValue = int.Parse(strs[1]);

                        return new { isPressed, angleRawValue };
                    }

                    catch { return null; }
                })
                .Where(data => data != null)
                .Subscribe(data =>
                {
                    //  Subjectを発火させる。
                    this.isPressedSubject.OnNext(data.isPressed);
                    this.angleSubject.OnNext(this.ConvertRawValueToAngle(data.angleRawValue));
                })
                .AddTo(disposable);

            this.serialPort.Open();
            this.canListen = false;
            return true;
        }

        public void Dispose()
        {
            this.disposable.Dispose();
        }

        private float ConvertRawValueToAngle(int rawValue)
        {
            //  まだ実装しない。
            return rawValue;
        }
    }
}