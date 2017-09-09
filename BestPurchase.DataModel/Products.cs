using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPurchase.DataModel
{
    // An entity for storing product list for business layer
    [Serializable]
    public class Products : EntityBase
    {
        public List<Product> ProductList { get; set; }
        public Products()
        {
            ProductList = new List<Product>();
        }
    }
}
