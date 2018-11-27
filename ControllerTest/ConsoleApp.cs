using System;
using System.IO.Ports;
using System.Reactive.Linq;
using Reactive;
using Reactive.Bindings.Extensions;

namespace ControllerTest
{
    public static class ConsoleApp
    {
        public static void Main()
        {
            using (var serialPort = new SerialPort("COM3", 9600))
            {
                serialPort.Open();

                Observable.Interval(TimeSpan.FromSeconds(0.015))
                    .Select(_ =>
                    {
                        //  手動でバッファを消す。
                        var data = serialPort.ReadLine();
                        serialPort.DiscardInBuffer();
                        return data;
                    })
                    .Select(str =>
                    {
                        try
                        {
                            var strs = str.Split(',');
                            var isClicked = (strs[0].Trim() == "1") ? true : false;
                            var regValue = int.Parse(strs[1]);
                            return (isClicked, regValue);
                        }

                        catch
                        {
                            return (false, -1);
                        }
                    })
                    .Where(tuple => tuple.Item2 != -1)
                    //.Sample(TimeSpan.FromSeconds(1))
                    .Subscribe(tuple => Console.WriteLine($"{(tuple.Item1 ? "ON" : "OFF")}, {tuple.Item2}"));

                while (true) {
                }
            }
        }
    }
}