using System;
using System.Net;
using Cysharp.Threading.Tasks;
using uDesktopMascot.Web.Application;
using uDesktopMascot.Web.Domain;

namespace uDesktopMascot.Web.Application
{
    /// <summary>
    ///  ボイス再生ハンドラ
    /// </summary>
    public class PlayVoiceHandler : IRequestHandler
    {
        /// <summary>
        ///  ボイス再生ユースケース
        /// </summary>
        private readonly PlayVoiceUseCase _useCase;

        /// <summary>
        ///  コンストラクタ
        /// </summary>
        /// <param name="useCase">ボイス再生ユースケース</param>
        public PlayVoiceHandler(PlayVoiceUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        ///  ボイス再生リクエストを処理できるかどうか
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>処理できるかどうか</returns>
        public bool CanHandle(HttpListenerRequest request)
        {
            return request.Url.AbsolutePath.StartsWith("/api/voice/");
        }

        /// <summary>
        ///  ランダムボイス再生
        /// </summary>
        /// <param name="context">コンテキスト</param>
        public Action<HttpListenerContext> PlayRandomVoice() => context => HandlePlayRandomVoiceRequest(context);

        /// <summary>
        ///  ランダムボイス再生リクエストを処理する
        /// </summary>
        /// <param name="context">コンテキスト</param>
        private async void HandlePlayRandomVoiceRequest(HttpListenerContext context)
        {
            await UniTask.SwitchToMainThread();
            _useCase.PlayRandom(context);
        }
    }
}
