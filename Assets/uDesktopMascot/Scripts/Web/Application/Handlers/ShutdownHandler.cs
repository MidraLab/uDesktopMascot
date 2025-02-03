using System;
using System.Net;
using Cysharp.Threading.Tasks;
using uDesktopMascot.Web.Domain;
using uDesktopMascot.Web.Application;

namespace uDesktopMascot.Web.Application
{
    /// <summary>
    ///  シャットダウンハンドラ
    /// </summary>
    public class ShutdownHandler : IRequestHandler
    {
        /// <summary>
        ///  シャットダウンユースケース
        /// </summary>
        private readonly ShutdownUseCase _useCase;

        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="useCase">シャットダウンユースケース</param>
        public ShutdownHandler(ShutdownUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        ///  リクエストを処理できるかどうか
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>処理できるかどうか</returns>
        public bool CanHandle(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.Equals("/api/shutdown");
        }

        /// <summary>
        ///  シャットダウンリクエストを処理する
        /// </summary>
        /// <param name="context">コンテキスト</param>
        public Action<HttpListenerContext> Shutdown() => context => HandleShutdownRequest(context);

        /// <summary>
        ///  シャットダウンリクエストを処理する
        /// </summary>
        /// <param name="context">コンテキスト</param>
        private async void HandleShutdownRequest(HttpListenerContext context)
        {
            await UniTask.SwitchToMainThread();
            _useCase.Shutdown(context);
        }
    }
}