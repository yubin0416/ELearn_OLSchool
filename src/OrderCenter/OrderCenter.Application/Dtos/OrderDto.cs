using OrderCenter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderCenter.Application.Dtos
{
    public class OrderDto
    {
        public string ID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CurriculumID { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CurriculumTitle { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string TransationID { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// 课程价格
        /// </summary>
        public decimal CurriculumPrice { get; set; }
        /// <summary>
        /// 优惠价格
        /// </summary>
        public decimal DiscountsPrice { get; set; }
        /// <summary>
        /// 实际支付
        /// </summary>
        public decimal ActualPayment { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus OrderStatus { get; set; }
    }
}
