using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMW.Website.Api.MsGraphApi.Core
{
    internal static class ApiConnector
    {
        #region RequestBase

        public static RequestReturn<T> Get<T>(string action, string jsonParameter = "")
        {
            return NewRequest<T>("GET", action, null, 6000, jsonParameter);
        }

        public static RequestReturn<T> Post<T>(string action, object bodyContent = null, string jsonParameter = "")
        {
            return NewRequest<T>("POST", action, bodyContent, 6000, jsonParameter);
        }

        public static RequestReturn<T> Patch<T>(string action, object bodyContent = null, string jsonParameter = "")
        {
            return NewRequest<T>("PATCH", action, bodyContent, 6000, jsonParameter);
        }

        public static Task<RequestReturn<T>> GetAsync<T>(string action)
        {
            return Task<RequestReturn<T>>.Factory.StartNew(() => NewRequest<T>("GET", action));
        }

        public static Task<RequestReturn<T>> PostAsync<T>(string action, object bodyContent = null)
        {
            return Task<RequestReturn<T>>.Factory.StartNew(() => NewRequest<T>("POST", action, bodyContent));
        }

        public static Task<RequestReturn<T>> PatchAsync<T>(string action, object bodyContent = null)
        {
            return Task<RequestReturn<T>>.Factory.StartNew(() => NewRequest<T>("PATCH", action, bodyContent));
        }

        /// <summary>
        ///     Makes a new request to the ms server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">action to call</param>
        /// <param name="url"></param>
        /// <param name="bodyContent">parameter for the action</param>
        /// <param name="timeOut">connection timeout</param>
        /// <param name="jsonParameter"></param>
        /// <returns></returns>
        private static RequestReturn<T> NewRequest<T>(string method, string url, object bodyContent = null,
            int timeOut = 6000, string jsonParameter = "")
        {
            var startDate = DateTime.Now;
            try
            {
                if (string.IsNullOrEmpty(url))
                    return RequestReturn<T>.Error("parameter required");

                var request = WebRequest.Create(Settings.ApiUrl + url);
                request.Method = method;
                request.ContentType = "application/json";
                request.Timeout = timeOut;
                request.Headers.Add("Authorization",
                    "Bearer " + TokenHandler.CurrentAccessToken); // set the authorisation token

                // write the body to the stream
                if (bodyContent != null)
                {
                    var bodyData = JsonConvert.SerializeObject(bodyContent);
                    var body = Encoding.UTF8.GetBytes(bodyData);
                    request.ContentLength = body.Length;

                    using var writer = request.GetRequestStream();
                    writer.Write(body, 0, body.Length);
                }
                else
                {
                    request.ContentLength = 0;
                }

                // read the response from the stream
                var response = request.GetResponse();
                var content = ResponseReader.ReadContentFromWebResponse<T>(response, jsonParameter);

                return new RequestReturn<T>(content, DateTime.Now - startDate,
                    RequestStatus.Success, "Request successfully");
            }
            catch (WebException webEx)
            {
                var exResponse = webEx.Response;
                if (exResponse is not HttpWebResponse)
                    return RequestReturn<T>.Error("unknown server error");
                var content = ResponseReader.ReadContentFromWebResponse(exResponse);
                return RequestReturn<T>.Error(!string.IsNullOrEmpty(content) ? content : "unknown server error");
            }
            catch (Exception ex)
            {
                return RequestReturn<T>.Error(ex.Message);
            }
        }

        #endregion
    }
}