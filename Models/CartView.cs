using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgMvcAdmin.Models.Common;

namespace AgMvcAdmin.Models
{
    public class CartView
    {
        public int CartId { get; set; }

        public int ItemCount { get; set; }
        public List<CartModel> CartItems { get; set; }

        public CartView() { }

        public CartView(int ct, List<CartModel> i) {
            ItemCount = ct;
            CartItems = i;
        }
    }
}