using System;
using System.Collections.Generic;
using System.Text;
using DDD.Framework;
using OrderCenter.Domain.DomianEvent;
using OrderCenter.Domain.Exceptions;

namespace OrderCenter.Domain
{
    public class Order:Entity<string>,IAggregateRoot
    {
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

        public  Order()
        {

        }

        public Order(string _UserID, string _CurriculumID,string _CurriculumTitle,decimal _CurriculumPrice,decimal _DiscountsPrice = 0) :this()
        {
            ID = Guid.NewGuid().ToString();
            UserID = _UserID;
            CurriculumID = _CurriculumID;
            CurriculumTitle = _CurriculumTitle;
            CurriculumPrice = _CurriculumPrice;
            DiscountsPrice = _DiscountsPrice;
            CreateDate = DateTime.Now;
            if (DiscountsPrice < 0)
            {
                throw new OrderDomainException("DiscountsPrice 不能小于0");
            }
            if (CurriculumPrice < 0)
            {
                throw new OrderDomainException("CurriculumPrice 不能小于0");
            }
            else if(CurriculumPrice == 0)
            {
                OrderStatus = OrderStatus.Payed;
                PaymentDate =DateTime.Now;
                AddDomianEvent(new CreateFreeOrderDomainEvent(this));
                return;
            }

            OrderStatus = OrderStatus.Created;
            AddDomianEvent(new CreateOrderDomainEvent(this));
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="TransationID"></param>
        /// <param name="PaymentMoney"></param>
        public void Paymented(string _TransationID,decimal _PaymentMoney)
        {
            if (OrderStatus != OrderStatus.Created)
            {
                throw new OrderDomainException("订单状态有误");
            }
            TransationID = _TransationID;
            ActualPayment = _PaymentMoney;
            if (ActualPayment != CurriculumPrice - DiscountsPrice)
            {
                throw new OrderDomainException("支付金额不对");
            }
            OrderStatus = OrderStatus.Payed;
            PaymentDate = DateTime.Now;
            AddDomianEvent(new PaidOrderDomainEvent(this));
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        public void CloseOrder()
        {
            if (OrderStatus != OrderStatus.Closed)
            {
                throw new OrderDomainException("订单状态有误");
            }
            OrderStatus = OrderStatus.Closed;
            AddDomianEvent(new ClosedOrderDomainEvent(this));
        }
    }
}
