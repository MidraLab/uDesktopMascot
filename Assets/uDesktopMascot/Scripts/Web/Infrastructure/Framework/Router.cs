using uDesktopMascot.Web.Domain.Interfaces;
using uDesktopMascot.Web.Application.Handlers;
using System.Net;
using uDesktopMascot;
using System.Threading;

namespace uDesktopMascot.Web.Infrastructure.Framework
{
    public class Router
    {
        private readonly NetWrapper _netWrapper;
        private readonly PlayVoiceHandler _playVoiceHandler;
        private readonly ShutdownHandler _shutdownHandler;

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
        /// <returns></returns>
        private void Init()
        {
            _netWrapper.GET("/api/voice/random", _playVoiceHandler.PlayRandomVoice());
            _netWrapper.GET("/api/shutdown", _shutdownHandler.Shutdown());
        }
    }
}