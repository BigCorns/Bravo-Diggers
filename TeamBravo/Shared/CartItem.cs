using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo.Shared
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }

        public decimal Price { get; set; }

        public string imgUrl { get; set; }
    }
}
