using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MMW.Website.Api.MsGraphApi.Core
{
    /// <summary>
    /// Helper for reading Content from a response
    /// </summary>
    internal static class ResponseReader
    {
        /// <summary>
        /// Read the content as string from a response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ReadContentFromWebResponse(WebResponse response) {
            try {
                var content = "";
                using var reader = new StreamReader(response.GetResponseStream());
                content = reader.ReadToEnd();
                return content;
            }
            catch (Exception ex) {
                Console.WriteLine("Api Call to '" + response.ResponseUri + "' throw an exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Read the content from a a response an deserialize it to the given Type
        /// </summary>
        /// <param name="response"></param>
        /// <param name="jsonParameter"></param>
        /// <returns></returns>
        public static T ReadContentFromWebResponse<T>(WebResponse response, string jsonParameter = "") {
            try
            {
                switch (response.ContentType)
                {
                    case "application/octet-stream":
                        return (T)Convert.ChangeType(response.GetResponseStream(), typeof(T));
                    case "application/pdf" when typeof(T) == typeof(byte[]):
                    {
                        // copy application/pdf stream to memory stream so we can convert it to a byte array
                        using var ms = new MemoryStream();
                        response.GetResponseStream()?.CopyTo(ms);
                        return (T)Convert.ChangeType(ms.ToArray(), typeof(T));
                    }
                    default:
                    {
                        var content = ReadContentFromWebResponse(response);

                        if (typeof(T) == typeof(string))
                            return (T)Convert.ChangeType(content, typeof(T)); // if T is string we don't need to serialize it
                        if (string.IsNullOrEmpty(content)) return default(T);
                        if (!string.IsNullOrEmpty(jsonParameter))
                            return ExtractPropertyAsObject<T>(content, jsonParameter);
                        var error = JsonConvert.DeserializeObject<T>(content);
                        return error;
                    }
                }
            }
            catch (Exception ex) {
                return default(T);
            }
        }

        private static T ExtractPropertyAsObject<T>(string content, string jsonParameter) {
            object jObjBase = JsonConvert.DeserializeObject(content);
            if(jObjBase == null)
                return default;

            var jObj = jObjBase as JObject;
            var parameters = jsonParameter.Split('/');
            JToken currentToken = null;
            for (var i = 0; i < parameters.Length; i++)
            {
                currentToken = i == 0 ? jObj[parameters[i]] : currentToken[parameters[i]];

                if (currentToken == null)
                    return default;
            }

            T error = JsonConvert.DeserializeObject<T>(currentToken.ToString());
            return error;
        }
    }
}
