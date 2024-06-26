using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.Models
{
    public class CartItem
    {
        public MusInstruments Item { get; set; }
        public int Qty { get; set; }
    }
}
