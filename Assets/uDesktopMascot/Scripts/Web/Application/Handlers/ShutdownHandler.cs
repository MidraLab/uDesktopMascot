using uDesktopMascot.Web.Domain.Interfaces;
using uDesktopMascot.Web.Application.UseCases;
using System;
using System.Net;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace uDesktopMascot.Web.Application.Handlers
{
    public class ShutdownHandler : IRequestHandler
    {
        private readonly ShutdownUseCase _useCase;

        public ShutdownHandler(ShutdownUseCase useCase)
        {
            _useCase = useCase;
        }

        public bool CanHandle(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.Equals("/api/shutdown");
        }

        public Action<HttpListenerContext> Shutdown() => context =>
        {
            UniTask.Void(async () =>
            {
                await UniTask.SwitchToMainThread();
                _useCase.Shutdown(context);
            });
        };
    }
}