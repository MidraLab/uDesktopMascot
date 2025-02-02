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

        public Router(NetWrapper netWrapper, PlayVoiceHandler playVoiceHandler)
        {
            _netWrapper = netWrapper;
            _playVoiceHandler = playVoiceHandler;
            Init();
        }

        /// <summary>
        /// ルーティングの初期化
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            _netWrapper.GET("/api/voice/random", _playVoiceHandler.PlayRandomVoice());
        }
    }
} 