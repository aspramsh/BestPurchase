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
        public Products Ordered { get; set; }
        public User Orderer { get; set; }
        public Order()
        {
            Ordered = new Products();
            Orderer = new User();
            Quantity = new List<int?>();
        }
        public DateTime OrderDate { get; set; }
        // Remember to change this to double
        public List<int?> Quantity { get; set; }
    }
}
