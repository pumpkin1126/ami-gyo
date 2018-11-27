using System;
using UniRx;
using UnityEngine;

namespace Amigyo.Input
{
    /// <summary>
    /// 入力ソースを提供します。
    /// 開始時と終了時にはそれぞれ<see cref="Listen"/>と<see cref="Dispose"/>を必ず呼び出してください。
    /// </summary>
    public interface IInputProvider : IDisposable
    {
        /// <summary>
        /// アミの発射角度を表す角度を通知・保持します。
        /// 角度はX軸正向きを始線とする極座標の角度で、度数法表記の値です。
        /// </summary>
        ReactiveProperty<float> Angle { get; }

        /// <summary>
        /// ボタンが押されているかどうかを通知・保持します。
        /// </summary>
        ReactiveProperty<bool> IsPressed { get; }

        /// <summary>
        /// 変更の通知を開始します。
        /// </summary>
        /// <returns>成功したかどうかを表すbool値。</returns>
        bool Listen();
    }
}