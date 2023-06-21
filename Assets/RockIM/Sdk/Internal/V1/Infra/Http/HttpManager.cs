using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Data;
using RockIM.Sdk.Utils;
using RockIM.Shared;
using MetaData = RockIM.Sdk.Internal.V1.Domain.Data.MetaData;

namespace RockIM.Sdk.Internal.V1.Infra.Http
{
    public class HttpManager : IHttpManager
    {
        private const string ServerHeaderTraceID = "RockIM-Server-TraceID";
        private const string ServerHeaderVersion = "RockIM-Server-Version";

        private readonly HttpClient _client;

        private const string ContentType = MimeUtils.Protobuf;


        public HttpManager(SdkContext context)
        {
            var filters = new List<IFilter>
            {
                new SignFilter(context.Config),
            };
            if (context.Authorization != null)
            {
                filters.Add(new AuthFilter(context.Authorization));
            }

            _client = new HttpClient(new CustomHttpClientHandler(filters));
            _client.BaseAddress = new Uri(context.Config.APIConfig.ServerUrl);
        }

        public Result<T> Call<T>(string action, IMessage req) where T : IMessage, new()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(action);
            request.Headers.Add("Accept", ContentType);
            request.Content = MimeUtils.IsJson(ContentType) ? new StringContent(JsonUtils.ToJson(req)) : new ByteArrayContent(req.ToByteArray());

            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var ret = _client.SendAsync(request);
            ret.Wait();
            if (!ret.IsCompleted)
            {
                var result = new Result<T>
                {
                    Code = ResultCode.ClientClosed
                };
                return result;
            }

            return DecodeResult<T>(ret.Result);
        }


        private static Result<T> DecodeResult<T>(HttpResponseMessage response) where T : IMessage, new()
        {
            var contentAsync = response.Content.ReadAsByteArrayAsync();
            contentAsync.Wait();
            var contentBytes = contentAsync.Result;
            var contentString = Encoding.Default.GetString(contentBytes);
            var result = new Result<T>
            {
                Meta = new MetaData(),
                Code = ResultCodeExtensions.FormInt((int) response.StatusCode)
            };
            // 读取响应头，获取traceId,version
            if (response.Headers.TryGetValues(ServerHeaderVersion, out var version))
            {
                result.Meta.Version = version.FirstOrDefault();
            }

            if (response.Headers.TryGetValues(ServerHeaderTraceID, out var traceId))
            {
                result.Meta.TraceId = traceId.FirstOrDefault();
            }

            var isJson = MimeUtils.IsJson(response.Content.Headers.ContentType.MediaType);
            if (!response.IsSuccessStatusCode)
            {
                var error = new Error();
                if (isJson)
                {
                    error = JsonUtils.FromJson<Error>(contentString);
                }
                else
                {
                    error.MergeFrom(contentAsync.Result);
                }

                result.Code = ResultCodeExtensions.FormInt(error.Code);
                result.Message = error.Message;
                result.Reason = error.Reason;
                return result;
            }

            if (isJson)
            {
                var data = JsonUtils.FromJson<T>(contentString);
                result.Data = data;
            }
            else
            {
                var message = new T();
                message.MergeFrom(contentAsync.Result);
                result.Data = message;
            }

            return result;
        }


        /// <summary>
        /// 签名
        /// </summary>
        private class SignFilter : IFilter
        {
            private const string ClientHeaderProductId = "RockIM-Client-ProductId";
            private const string ClientHeaderTimestamp = "RockIM-Client-Timestamp";
            private const string ClientHeaderNonce = "RockIM-Client-Nonce";
            private const string ClientHeaderSign = "RockIM-Client-Sign";

            private readonly SdkConfig _sdkConfig;

            public SignFilter(SdkConfig sdkConfig)
            {
                _sdkConfig = sdkConfig;
            }

            public void DoFilter(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var productId = _sdkConfig.APIConfig.ProductId;
                var ts = DateUtils.NowTs().ToString();
                var nonce = Guid.NewGuid().ToString();
                var dict = new Dictionary<string, string>()
                {
                    {"productId", productId},
                    {"nonce", nonce},
                    {"timestamp", ts},
                };
                var sign = Sign(dict, _sdkConfig.APIConfig.ProductKey);
                request.Headers.Add(ClientHeaderProductId, productId);
                request.Headers.Add(ClientHeaderTimestamp, ts);
                request.Headers.Add(ClientHeaderNonce, nonce);
                request.Headers.Add(ClientHeaderSign, sign);
            }

            private static string Sign(Dictionary<string, string> values, string productKey)
            {
                var keys = values.Keys.Where(key => key != "sign").ToList();
                keys.Sort();

                var buf = new StringBuilder();
                for (var i = 0; i < keys.Count; i++)
                {
                    if (i > 0)
                    {
                        buf.Append("&");
                    }

                    buf.Append(keys[i]);
                    buf.Append("=");
                    buf.Append(values[keys[i]]);
                }

                buf.Append("&");
                buf.Append(productKey);
                return DigestUtils.Md5(buf.ToString()).ToUpper();
            }
        }

        /// <summary>
        /// 授权
        /// </summary>
        private class AuthFilter : IFilter
        {
            private const string ClientHeaderAccessToken = "RockIM-Client-AccessToken";

            private readonly Authorization _authorization;

            public AuthFilter(Authorization authorization)
            {
                _authorization = authorization;
            }

            public void DoFilter(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add(ClientHeaderAccessToken, _authorization.AccessToken);
            }
        }
    }


    internal interface IFilter
    {
        public void DoFilter(HttpRequestMessage request, CancellationToken cancellationToken);
    }

    internal class CustomHttpClientHandler : HttpClientHandler
    {
        private readonly List<IFilter> _filters;

        public CustomHttpClientHandler(List<IFilter> filters)
        {
            _filters = filters;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_filters == null) return base.SendAsync(request, cancellationToken);
            foreach (var filter in _filters)
            {
                filter.DoFilter(request, cancellationToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}