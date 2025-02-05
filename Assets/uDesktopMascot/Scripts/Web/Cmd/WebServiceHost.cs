using Unity.Logging;
using System;
using uDesktopMascot.Web.Application;
using uDesktopMascot.Web.Infrastructure;

namespace uDesktopMascot.Web.Cmd
{
    /// <summary>
    ///   Webサーバーのホストクラス
    /// </summary>
    public class WebServiceHost : IDisposable
    {
        /// <summary>
        ///  ネットワークラッパー
        /// </summary>
        private NetWrapper _netWrapper;

        /// <summary>
        ///  ポート番号
        /// </summary>
        private int _port;

        /// <summary>
        ///  サーバー開始
        /// </summary>
        public void Start()
        {
            // 既に実行中の場合
            if (_netWrapper != null)
            {
                Log.Error("Webサーバーは既に実行中です。");
                return;
            }

            // ルーターの設定
            _netWrapper = new NetWrapper();

            // 依存関係の初期化
            var playVoiceUseCase = new PlayVoiceUseCase();
            var playVoiceHandler = new PlayVoiceHandler(playVoiceUseCase);
            var shutdownUseCase = new ShutdownUseCase();
            var shutdownHandler = new ShutdownHandler(shutdownUseCase);

            var router = new Router(_netWrapper, playVoiceHandler, shutdownHandler);

            // サーバーの起動
            _port = _netWrapper.GetAvailablePort();
            _netWrapper.StartServer(_port);
            Log.Info($"Webサーバーが起動しました。ポート: {_port}");
        }

        /// <summary>
        ///  サーバーのポート番号を取得
        /// </summary>
        /// <returns>ポート番号</returns>
        public int GetPort()
        {
            return _port;
        }

        /// <summary>
        ///  破棄
        /// </summary>
        public void Dispose()
        {
            _netWrapper.Dispose();
            _netWrapper = null;
        }
    }
}
