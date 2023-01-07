using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cafe : IEntity
    {
        public int CafeId { get; set; }
        public string CafeName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
