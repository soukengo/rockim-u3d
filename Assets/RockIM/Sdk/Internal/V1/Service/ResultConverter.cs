using System;
using RockIM.Sdk.Api.V1.Dtos;
using RockIM.Sdk.Internal.V1.Domain.Data;
using MetaData = RockIM.Sdk.Api.V1.Dtos.MetaData;

namespace RockIM.Sdk.Internal.V1.Service
{
    public abstract class ResultConverter
    {
        /// <summary>
        /// 转换结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="converter"></param>
        /// <typeparam name="TS">source</typeparam>
        /// <typeparam name="TT">target</typeparam>
        /// <returns></returns>
        public static APIResult<TT> Convert<TS, TT>(Result<TS> result, Func<TS, TT> converter) where TT : new()
        {
            var ret = new APIResult<TT>
            {
                Code = result.Code,
                Message = result.Message,
                Reason = result.Reason,
            };
            if (result.Meta != null)
            {
                ret.Meta = new MetaData
                {
                    TraceId = result.Meta.TraceId,
                    Version = result.Meta.Version,
                };
            }

            if (!result.IsSuccess())
            {
                return ret;
            }

            ret.Data = converter(result.Data);
            return ret;
        }
    }
}