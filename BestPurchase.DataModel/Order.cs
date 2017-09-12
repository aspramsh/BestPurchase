using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPurchase.DataModel
{
    [Serializable]
    public class Order : EntityBase
    {
        public int Id { get; set; }
        public Product Ordered { get; set; }
        public User Orderer { get; set; }
        public Order()
        {
            Ordered = new Product();
            Orderer = new User();
        }
        public DateTime OrderDate { get; set; }
        int Quantity { get; set; }
    }
}
