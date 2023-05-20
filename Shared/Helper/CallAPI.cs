using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Model.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Shared.Helper
{
    /// <summary>
    /// Call api httpclient
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public static class CallApi<T, R> where R : class
    {
        public static async Task<(R?, ErrorModel)> GetAsJsonAsync(T value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {
                var client = option.Client;

                if (!string.IsNullOrEmpty(uri))
                {
                    client.BaseAddress = new Uri(uri);
                }

                client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(option.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                string urlQueryString = value == null ? path : string.Format("{0}?{1}", path, ParseModelToQueryString(value));
                ///var contractJson = JsonConvert.SerializeObject(contract);
                HttpResponseMessage httpResponseMessage = await client.GetAsync(urlQueryString, cancellationToken);

                ///wrire error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;
                /// throws an exception if errors when posting

                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;

                Task<R> response = mediaType.Contains("text/html")
                      ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                      : httpResponseMessage.Content.ReadAsStreamAsync<R>();
                return (await response, errorModel);
            }
            catch (Exception ex)
            {
                errorModel.Message = ex.Message;
                return (null, errorModel);
            }
            finally
            {
                option.Client.Dispose();
            }
        }

        public static async Task<(R?, ErrorModel, HttpResponseHeaders?)> GetAsJsonAndHeaderAsync(T value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {
                var client = option.Client;

                if (!string.IsNullOrEmpty(uri))
                {
                    client.BaseAddress = new Uri(uri);
                }

                client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(option.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                string urlQueryString = value == null ? path : string.Format("{0}?{1}", path, ParseModelToQueryString(value));
                ///var contractJson = JsonConvert.SerializeObject(contract);
                HttpResponseMessage httpResponseMessage = await client.GetAsync(urlQueryString, cancellationToken);

                ///wrire error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;
                /// throws an exception if errors when posting

                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;

                Task<R> response = mediaType.Contains("text/html")
                      ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                      : httpResponseMessage.Content.ReadAsStreamAsync<R>();
                return (await response, errorModel, httpResponseMessage.Headers);
            }
            catch (Exception ex)
            {
                errorModel.Message = ex.Message;
                return (null, errorModel, null!);
            }
            finally
            {
                option.Client.Dispose();
            }
        }

        /// <summary>
        /// fetch data to a specific address
        /// </summary>
        /// <param name="value"></param>
        /// <param name="uri"></param>
        /// <param name="path"></param>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="token"></param>
        /// <param name="authType"></param>
        public static async Task<(R?, ErrorModel)> PostAsJsonAsync(T value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {

                if (!string.IsNullOrEmpty(uri))
                {
                    option.Client.BaseAddress = new Uri(uri);
                }

                option.Client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                if (!string.IsNullOrEmpty(option.Token))
                {
                    option.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                option.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = System.Text.Json.JsonSerializer.Serialize(value);
                StringContent httpContent = new(json, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await option.Client.PostAsync(path, httpContent, cancellationToken);
                ///write error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;

                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;
                Task<R> response = mediaType.Contains("text/html")
                     ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                     : httpResponseMessage.Content.ReadAsStreamAsync<R>();

                return (await response, errorModel);
            }
            catch (Exception ex)
            {
                errorModel.Message = new StringBuilder(errorModel.Message).Append(' ').Append(ex.Message).ToString();
                return (null, errorModel);
            }finally {
                option.Client.Dispose();
            }
        }

        public static async Task<(R?, ErrorModel)> PostFormAsync(Dictionary<string, string> value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {

                if (!string.IsNullOrEmpty(uri))
                {
                    option.Client.BaseAddress = new Uri(uri);
                }

                option.Client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                if (!string.IsNullOrEmpty(option.Token))
                {
                    option.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                FormUrlEncodedContent content = new(value);

                HttpResponseMessage httpResponseMessage = await option.Client.PostAsync(path, content, cancellationToken);
                ///write error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;
                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;

                Task<R> response = mediaType.Contains("text/html")
                      ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                      : httpResponseMessage.Content.ReadAsStreamAsync<R>();
                return (await response, errorModel);
            }
            catch (Exception ex)
            {
                errorModel.Message = new StringBuilder(errorModel.Message).Append(' ').Append(ex.Message).ToString();
                return (null, errorModel);
            }
            finally
            {
                option.Client.Dispose();
            }
        }

        public static async Task<(R?, ErrorModel)> PutAsJsonAsync(T value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {
                if (!string.IsNullOrEmpty(uri))
                {
                    option.Client.BaseAddress = new Uri(uri);
                }

                option.Client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                option.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(option.Token))
                {
                    option.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                string json = System.Text.Json.JsonSerializer.Serialize(value);
                StringContent httpContent = new(json, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await option.Client.PutAsync(path, httpContent, cancellationToken);
                ///wrire error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;

                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;

                Task<R> response = mediaType.Contains("text/html")
                       ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                       : httpResponseMessage.Content.ReadAsStreamAsync<R>();
                return (await response, errorModel);
            }
            catch (Exception ex)
            {
                errorModel.Message = new StringBuilder(errorModel.Message).Append(" ").Append(ex.Message).ToString();
                return (null, errorModel);
            }
            finally
            {
                option.Client.Dispose();
            }
        }

        public static async Task<(R?, ErrorModel)> DeleteAsJsonAsync(T value, string uri, string path, HttpOption option, CancellationToken cancellationToken)
        {
            ErrorModel errorModel = new();
            try
            {
                if (!string.IsNullOrEmpty(uri))
                {
                    option.Client.BaseAddress = new Uri(uri);
                }

                option.Client.Timeout = TimeSpan.FromMinutes(option.Timeout);

                option.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(option.Token))
                {
                    option.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(option.AuthType, option.Token);
                }

                string urlQueryString = value == null ? path : string.Format("{0}?{1}", path, ParseModelToQueryString(value));
                ///var contractJson = JsonConvert.SerializeObject(contract);
                HttpResponseMessage httpResponseMessage = await option.Client.DeleteAsync(urlQueryString, cancellationToken);
                ///wrire error to error model
                errorModel.StatusCode = (int)httpResponseMessage.StatusCode;
                errorModel.Message = httpResponseMessage.ReasonPhrase;
                errorModel.Succeeded = httpResponseMessage.IsSuccessStatusCode;
                string mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    Task<R> response = mediaType.Contains("text/html")
                       ? httpResponseMessage.Content.ReadRawContentAsync<R>()
                       : httpResponseMessage.Content.ReadAsStreamAsync<R>();
                    return (await response, errorModel);
                }

                return (null, errorModel);
            }
            catch (Exception ex)
            {
                errorModel.Message = new StringBuilder(errorModel.Message).Append(" ").Append(ex.Message).ToString();
                return (null, errorModel);
            }
            finally
            {
                option.Client.Dispose();
            }
        }

        /// <summary>
        /// Parse data to queryString
        /// </summary>
        /// <param name="data"></param>
        private static string ParseModelToQueryString(T data)
        {
            StringBuilder result = new();

            Type type = data.GetType();
            foreach (PropertyInfo item in type.GetProperties())
            {
                object value = item.GetValue(data, null);
                if (value != null)
                {
                    _ = typeof(string).Equals(value.GetType())
                        ? result.AppendFormat("{0}={1}&", item.Name, HttpUtility.UrlEncode(value.ToString()))
                        : result.AppendFormat("{0}={1}&", item.Name, value.ToString());
                }
            }

            if (result.ToString().EndsWith("&"))
            {
                StringBuilder resultCut = new();
                _ = resultCut.Append(result.ToString(), 0, result.Length - 1);

                return resultCut.ToString();
            }

            return result.ToString();
        }
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static async Task<T> ReadAsStreamAsync<T>(this HttpContent content)
        {
            Stream stream = await content.ReadAsStreamAsync();

            if (stream.Length == 0)
            {
                return default;
            }

            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static async Task<T> ReadRawContentAsync<T>(this HttpContent content)
        {
            Task<string> strResult = content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(await strResult);
            }
            catch (Exception e)
            {
                throw new JsonReaderException(e.Message, e);
            }
        }
    }
}