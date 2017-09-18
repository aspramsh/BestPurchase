using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestPurchase.ServiceLayer.Models
{
    public class OrderModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-Mail is required")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<CartModel> Cart { get; set; }
        public OrderModel()
        {
            Cart = new List<CartModel>();
        }
    }
}