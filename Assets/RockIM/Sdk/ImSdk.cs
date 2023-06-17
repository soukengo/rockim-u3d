using System;
using System.Threading.Tasks;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Constants;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Api.V1.Exceptions;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1;
using UnityEngine;

namespace RockIM.Sdk
{
    public abstract class ImSdk
    {
        private static readonly Lazy<Client> V1Lazy = new(new ClientV1());
        public static Client V1 => V1Lazy.Value;


        /// <summary>
        /// 同步调用
        /// </summary>
        /// <param name="syncMethod"></param>
        /// <typeparam name="T"></typeparam>
        public static APIResult<T> Call<T>(Func<APIResult<T>> syncMethod)
        {
            APIResult<T> ret;
            try
            {
                ret = syncMethod();
            }
            catch (BusinessException e)
            {
                ret = new APIResult<T>
                {
                    Code = ResultCode.BadRequest,
                    Reason = e.Reason,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                Debug.LogError("exception: " + e.StackTrace);
                ret = new APIResult<T>
                {
                    Code = ResultCode.Unknown,
                    Reason = ErrorReasons.ClientException,
                    Message = e.StackTrace
                };
            }

            Debug.Log("Result: " + ret);
            return ret;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <param name="syncMethod"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public static void Async<T>(Func<APIResult<T>> syncMethod, Action<APIResult<T>> callback)
        {
            Task.Run(() =>
            {
                var ret = Call(syncMethod);
                AsyncManager.Callback(() => callback(ret));
            });
        }
    }
}