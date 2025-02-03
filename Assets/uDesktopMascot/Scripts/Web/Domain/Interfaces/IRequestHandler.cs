using System.Net;
using System;

namespace uDesktopMascot.Web.Domain
{
    /// <summary>
    ///  リクエストハンドラ
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        ///  リクエストを処理できるかどうか
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>処理できるかどうか</returns>
        bool CanHandle(HttpListenerRequest request);
    }
}