using Unity.Logging;
using System;
using uDesktopMascot.Web.Infrastructure.Framework;
using uDesktopMascot.Web.Application;

namespace uDesktopMascot.Web.Cmd
{
    /// <summary>
    ///  メインクラス
    /// </summary>
    public class WebServiceHost : IDisposable
    {
        /// <summary>
        ///  ネットワークラッパー
        /// </summary>
        private NetWrapper _netWrapper;

        /// <summary>
        ///  ポート
        /// </summary>
        private int _port = 8080;

        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="port">ポート</param>
        public WebServiceHost(int port = 8080)
        {
            _port = port;
        }

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
            _netWrapper.StartServer(_port);
            Log.Info($"Webサーバーが起動しました。ポート: {_port}");
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
