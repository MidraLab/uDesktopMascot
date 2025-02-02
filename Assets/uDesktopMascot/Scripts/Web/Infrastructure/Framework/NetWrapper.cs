using System.Net;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using Unity.Logging;
using System;
using uDesktopMascot.Web.Domain.Interfaces;

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
        private readonly Dictionary<string, Action<HttpListenerContext>> _getHandlers = new();
        private readonly Dictionary<string, Action<HttpListenerContext>> _postHandlers = new();
        private readonly Dictionary<string, Action<HttpListenerContext>> _putHandlers = new();
        private readonly Dictionary<string, Action<HttpListenerContext>> _deleteHandlers = new();

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
        /// <param name="handlers">ハンドラ</param>
        private void RegisterHandler(string method, string path, Action<HttpListenerContext> handlerAction, Dictionary<string, Action<HttpListenerContext>> handlers)
        {
            handlers[path] = context =>
            {
                if (context.Request.HttpMethod.Equals(method, StringComparison.OrdinalIgnoreCase))
                {
                    handlerAction(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    context.Response.Close();
                }
            };
        }

        /// <summary>
        ///  GETハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void GET(string path, Action<HttpListenerContext> handlerAction)
        {
            RegisterHandler("GET", path, handlerAction, _getHandlers);
        }

        /// <summary>
        ///  POSTハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void POST(string path, Action<HttpListenerContext> handlerAction)
        {
            RegisterHandler("POST", path, handlerAction, _postHandlers);
        }

        /// <summary>
        ///  PUTハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void PUT(string path, Action<HttpListenerContext> handlerAction)
        {
            RegisterHandler("PUT", path, handlerAction, _putHandlers);
        }

        /// <summary>
        ///  DELETEハンドラを登録する
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="handlerAction">ハンドラアクション</param>
        public void DELETE(string path, Action<HttpListenerContext> handlerAction)
        {
            RegisterHandler("DELETE", path, handlerAction, _deleteHandlers);
        }

        /// <summary>
        ///  リクエストを処理する
        /// </summary>
        /// <param name="context">コンテキスト</param>
        private void ProcessRequest(HttpListenerContext context)
        {
            string path = context.Request.Url.AbsolutePath;
            string method = context.Request.HttpMethod.ToUpper();

            Dictionary<string, Action<HttpListenerContext>> handlers = method switch
            {
                "GET" => _getHandlers,
                "POST" => _postHandlers,
                "PUT" => _putHandlers,
                "DELETE" => _deleteHandlers,
                _ => null
            };

            if (handlers != null && handlers.TryGetValue(path, out var handler))
            {
                handler(context);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.Close();
            }
        }
    }
}