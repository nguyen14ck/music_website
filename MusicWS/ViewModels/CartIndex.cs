using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicWS.Models;

namespace MusicWS.ViewModels
{
    public class CartIndex
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public CartIndex(Cart Cart, string ReturnUrl)
        {
            this.Cart = Cart;
            this.ReturnUrl = ReturnUrl;
        }
    }
}