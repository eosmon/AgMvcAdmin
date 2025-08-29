using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgMvcAdmin.Models.Common
{
    public class CustomModel
    {
        public BoundBookModel Book { get; set; }

        public AmmoModel Ammo { get; set; }

        public GunModel Gun { get; set; }

        public MerchandiseModel Merch { get; set; }

        public CartModel Cart { get; set; }

        public FulfillModel Fulfill { get; set; }


        public CustomModel() { }

        public CustomModel(CartModel c, GunModel g, BoundBookModel b)
        {
            Cart = c;
            Gun = g;
            Book = b;
        }

        public CustomModel(CartModel c, AmmoModel a, BoundBookModel b)
        {
            Cart = c;
            Ammo = a;
            Book = b;
        }

        public CustomModel(CartModel c, MerchandiseModel m, BoundBookModel b)
        {
            Cart = c;
            Merch = m;
            Book = b;
        }

    }
}