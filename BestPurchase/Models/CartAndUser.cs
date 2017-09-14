using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestPurchase.ServiceLayer.Models
{
    public class CartAndUser
    {
        public List<CartModel> Cart { get; set; }
        [Required]
        public UserModel user { get; set; }
        public CartAndUser()
        {
            Cart = new List<CartModel>();
            user = new UserModel();
        }
    }
}