using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Coffee : IEntity
    {
        public int CoffeeId { get; set; }
        public int CafeId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Name { get; set; }
    }
}
