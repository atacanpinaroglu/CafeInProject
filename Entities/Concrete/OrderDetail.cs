using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int CoffeeId { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
    }
}
