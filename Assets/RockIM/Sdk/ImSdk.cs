using System;
using System.Threading.Tasks;
using RockIM.Sdk.Api.V1.Constants;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Api.V1.Exceptions;
using RockIM.Sdk.Framework;
using RockIM.Sdk.Internal.V1.Context;

namespace RockIM.Sdk
{
    public abstract class ImSdk
    {
        /// <summary>
        /// 同步调用
        /// </summary>
        /// <param name="syncMethod"></param>
        /// <typeparam name="T"></typeparam>
        protected static APIResult<T> Call<T>(Func<APIResult<T>> syncMethod)
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
                LoggerContext.Logger.Warn("exception: {0}", e);
                ret = new APIResult<T>
                {
                    Code = ResultCode.InternalError,
                    Reason = ErrorReasons.ClientException,
                    Message = e.StackTrace
                };
            }

            return ret;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <param name="syncMethod"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        protected static void Async<T>(Func<APIResult<T>> syncMethod, Action<APIResult<T>> callback)
        {
            Task.Run(() =>
            {
                var ret = Call(syncMethod);
                try
                {
                    AsyncManager.Execute(() => callback(ret));
                }
                catch (Exception e)
                {
                    LoggerContext.Logger.Warn("exception: {0}", e);
                }
            });
        }
    }
}