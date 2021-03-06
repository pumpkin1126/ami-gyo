﻿using System;
using UniRx;
using UnityEngine;

namespace Amigyo.Input
{
    public sealed class InputTester : MonoBehaviour
    {
        private readonly IInputProvider inputProvider = new DeviceInputProvider("COM3", 9600);

        private void Start()
        {
            inputProvider.Listen();
            inputProvider.Angle.Subscribe(angle => Debug.Log($"角度:{angle}")).AddTo(this);
            inputProvider.IsPressed.Where(flag => flag).Subscribe(_ => Debug.Log("押された")).AddTo(this);
        }

        private void OnDestroy()
        {
            this.inputProvider.Dispose();
        }
    }
}