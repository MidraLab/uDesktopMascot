using System.Net;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using Unity.Logging;
using System;
using uDesktopMascot.Web.Domain.Interfaces;
using System.Net.Http;

namespace uDesktopMascot.Web.Infrastructure.Framework
{
    /// <summary>
    ///  ネットワークラッパー
    /// </summary>
    public class NetWrapper : IDisposable
    {
        /// <summary>
        ///  リスナー
        /// </summary>
        private HttpListener _listener;

        /// <summary>
        ///  リスナースレッド
        /// </summary>
        private Thread _listenerThread;

        /// <summary>
        ///  ハンドラ
        /// </summary>
        private readonly Dictionary<HttpMethod, Dictionary<string, Action<HttpListenerContext>>> _handlers = new();

        /// <summary>
        ///  サーバーを起動する
        /// </summary>
        /// <param name="port">ポート</param>
        public void StartServer(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/");
            _listener.Start();

            _listenerThread = new Thread(StartListening);
            _listenerThread.Start();
        }

        /// <summary>
        ///  リスナーを起動する
        /// </summary>
        private void StartListening()
        {
            try
            {
                while (_listener?.IsListening == true)
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
            }
            catch (HttpListenerException)
            {
                Log.Info("Listener stopped normally");
            }
            catch (ObjectDisposedException)
            {
                Log.Info("Listener already disposed");
            }
        }

        /// <summary>
        ///  サーバーを停止する
        /// </summary>
        public void StopServer()
        {
            try
            {
                _listener?.Stop();
                _listenerThread?.Join(1000); // 最大1秒待機
                Log.Info("Listener stopped normally");
            }
            catch (ObjectDisposedException)
            {
                Log.Info("Listener already disposed");
            }
            finally
            {
                _listenerThread = null;
            }
        }

        /// <summary>
        ///  ディスポーズ
        /// </summary>
        public void Dispose()
        {
            StopServer();
            _listener?.Close();
            _listener = null;
        }

        /// <summary>
        ///  ハンドラを登録する
        /// </summary>
        /// <param name="method">メソッド</param>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        private void RegisterHandler(HttpMethod method, string path, Action<HttpListenerContext> handlerAction)
        {
            if (!_handlers.ContainsKey(method))
            {
                _handlers[method] = new Dictionary<string, Action<HttpListenerContext>>();
            }
            _handlers[method][path] = handlerAction;
        }

        /// <summary>
        ///  GETハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void GET(string path, Action<HttpListenerContext> handlerAction) => RegisterHandler(HttpMethod.Get, path, handlerAction);

        /// <summary>
        ///  POSTハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void POST(string path, Action<HttpListenerContext> handlerAction) => RegisterHandler(HttpMethod.Post, path, handlerAction);

        /// <summary>
        ///  PUTハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void PUT(string path, Action<HttpListenerContext> handlerAction) => RegisterHandler(HttpMethod.Put, path, handlerAction);

        /// <summary>
        ///  DELETEハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void DELETE(string path, Action<HttpListenerContext> handlerAction) => RegisterHandler(HttpMethod.Delete, path, handlerAction);

        /// <summary>
        ///  リクエストを処理する
        /// </summary>
        /// <param name="context">コンテキスト</param>
        private void ProcessRequest(HttpListenerContext context)
        {
            string path = context.Request.Url.AbsolutePath;

            var method = new HttpMethod(context.Request.HttpMethod);

            if (_handlers.TryGetValue(method, out var methodHandlers) && methodHandlers.TryGetValue(path, out var handler))
            {
                handler(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.Close();
            }
        }
    }
}