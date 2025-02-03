using System.Net;
using Cysharp.Threading.Tasks;
using uDesktopMascot.Web.Application;

namespace uDesktopMascot.Web.Infrastructure
{
    /// <summary>
    ///  ルーター
    /// </summary>
    public class Router
    {
        /// <summary>
        ///  ネットワークラッパー
        /// </summary>
        private readonly NetWrapper _netWrapper;

        /// <summary>
        ///  ボイス再生ハンドラ
        /// </summary>
        private readonly PlayVoiceHandler _playVoiceHandler;

        /// <summary>
        ///  シャットダウンハンドラ
        /// </summary>
        private readonly ShutdownHandler _shutdownHandler;

        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="netWrapper">ネットワークラッパー</param>
        /// <param name="playVoiceHandler">ボイス再生ハンドラ</param>
        /// <param name="shutdownHandler">シャットダウンハンドラ</param>
        public Router(NetWrapper netWrapper, PlayVoiceHandler playVoiceHandler, ShutdownHandler shutdownHandler)
        {
            _netWrapper = netWrapper;
            _playVoiceHandler = playVoiceHandler;
            _shutdownHandler = shutdownHandler;
            Init();
        }

        /// <summary>
        /// ルーティングの初期化
        /// </summary>
        private void Init()
        {
            _netWrapper.GET("/api/voice/random", _playVoiceHandler.PlayRandomVoice());
            _netWrapper.GET("/api/shutdown", _shutdownHandler.Shutdown());
        }
    }
}