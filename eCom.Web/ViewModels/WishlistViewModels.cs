using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Web.Models;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class WishlistViewModel
    {
        public ApplicationUser User { get; set; }

        public Wishlist Wishlist { get; set; }
    }
}