using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yq.WebApi.Model
{
    
    /// <summary>
    /// 所有接口都要返回这个类型的对象
    /// 
    /// </summary>
    public class MyJsonResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Result { get; set; }

    }
     enum State
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok = 200,
        /// <summary>
        /// 失败
        /// </summary>
        Error = 500
    };
}
