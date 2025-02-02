using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace uDesktopMascot.Web.Application.UseCases
{
    public class PlayVoiceUseCase
    {
        public void PlayRandom(HttpListenerContext context)
        {
            try
            {
                Log.Info("ランダムボイス再生リクエスト受信");
                VoiceController.Instance.PlayClickVoice();
                SendSuccessResponse(context, "ランダムボイスを再生しました");
            }
            catch (Exception e)
            {
                ReturnInternalError(context.Response, e);
            }
        }

        public void PlaySpecific(HttpListenerContext context)
        {
            try
            {
                Log.Info("特定ボイス再生リクエスト受信");
                VoiceController.Instance.PlayClickVoice();
                SendSuccessResponse(context, "特定ボイスを再生しました");
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
            var errorData = new { error = "内部エラーが発生しました", detail = e.Message };
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(errorData));

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            response.OutputStream.Write(bytes, 0, bytes.Length);
            response.Close();
        }
    }
}
