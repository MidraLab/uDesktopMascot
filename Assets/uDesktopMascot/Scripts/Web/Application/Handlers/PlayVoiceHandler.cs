using System;
using System.Net;
using uDesktopMascot.Web.Domain.Interfaces;
using uDesktopMascot.Web.Application.UseCases;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace uDesktopMascot.Web.Application.Handlers
{
    public class PlayVoiceHandler : IRequestHandler
    {
        private readonly PlayVoiceUseCase _useCase;

        public PlayVoiceHandler(PlayVoiceUseCase useCase)
        {
            _useCase = useCase;
        }

        public bool CanHandle(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.StartsWith("/api/voice/");
        }

        public Action<HttpListenerContext> PlayRandomVoice() => context =>
        {
            UniTask.Void(async () =>
            {
                await UniTask.SwitchToMainThread();
                _useCase.PlayRandom(context);
            });
        };
    }
}
