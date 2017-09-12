using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPurchase.DataModel
{
    [Serializable]
    public class ShoppingCart : EntityBase
    {
        public Product Added { get; set; }
        public int? Quantity { get; set; }

        public ShoppingCart()
        {
            Added = new Product();
        }
    }
}
