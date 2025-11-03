using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.OrderAggrecate
{
    public class Order :BaseEntity
    {
        public Order()
        {
            
        }

        public Order(string buyerEmial, Address shippingAddress, decimal subTotal, DeliveryMethod deliveryMethod, ICollection<OrderItem> items)
        {
            BuyerEmial = buyerEmial;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            this.deliveryMethod = deliveryMethod;
            Items = items;
        }

        public string BuyerEmial { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } // Kda 3lshan hya relation one to one byt3mlo f 1 table


        public decimal SubTotal { get; set; }
        [NotMapped]
        public decimal Total => SubTotal + deliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = "";   

        public DeliveryMethod deliveryMethod { get; set; }// Deh hatkom navegation prop one
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); //navigation prop meny


    }
}
