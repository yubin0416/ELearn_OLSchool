using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Domain
{
    public enum OrderStatus
    {
        /// <summary>
        /// 创建
        /// </summary>
        Created,
        /// <summary>
        /// 已支付
        /// </summary>
        Payed,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed
    }
}
