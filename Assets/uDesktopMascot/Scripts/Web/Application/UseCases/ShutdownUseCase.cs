using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using uDesktopMascot.Web.Infrastructure.Framework;

namespace uDesktopMascot.Web.Application.UseCases
{
    public class ShutdownUseCase
    {
        private readonly NetWrapper _netWrapper;

        public ShutdownUseCase(NetWrapper netWrapper)
        {
            _netWrapper = netWrapper;
        }

        public void Shutdown(HttpListenerContext context)
        {
            try
            {

                SendSuccessResponse(context, "サーバーを停止しました");

                _netWrapper.Dispose();

            }
            catch (Exception e)
            {
                ReturnInternalError(context.Response, e);
            }
        }

        private void SendSuccessResponse(HttpListenerContext context, string message)
        {
            var responseData = new { message };
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData));

            context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.Close();
        }

        private void ReturnInternalError(HttpListenerResponse response, Exception e)
        {
            var errorData = new { error = "シャットダウンに失敗しました", detail = e.Message };
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(errorData));

            response.AppendHeader("Access-Control-Allow-Origin", "*");
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            response.OutputStream.Write(bytes, 0, bytes.Length);
            response.Close();
        }
    }
}