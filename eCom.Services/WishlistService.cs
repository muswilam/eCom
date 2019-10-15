using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCom.Entities;
using eCom.Data;
using System.Data.Entity;

namespace eCom.Services
{
    public class WishlistService
    {
        #region Singleton Non-Thread Safety
        public static WishlistService Instance
        {
            get
            {
                if (instance == null) instance = new WishlistService();

                return instance;
            }
        }
        private static WishlistService instance { get; set; }
        private WishlistService()
        {
        }
        #endregion

        //create wishlist
        public bool SaveWishlist(Wishlist wishlist)
        {
            using (eComContext context = new eComContext())
            {
                context.Wishlists.Add(wishlist);
                return context.SaveChanges() > 0;
            }
        }

        //is there a wishlist by userId
        public bool IsExistWishlist(string userId)
        {
            using (eComContext context = new eComContext())
            {
                var existing = context.Wishlists.Where(w => w.UserId == userId).Include(w => w.WishlistItems).FirstOrDefault();
                return existing != null;
            }
        }

        //is there a product 
        public bool IsExistProduct(int productId, Wishlist existWishlist)
        {
            using (eComContext context = new eComContext())
            {
                bool wishlist = existWishlist.WishlistItems.Exists(w => w.ProductId == productId);
                return wishlist;
            }
        }

        //get wishlist by userId
        public Wishlist GetWishlist(string userId)
        {
            using (eComContext context = new eComContext())
            {
                return context.Wishlists.Where(w => w.UserId == userId).Include(w => w.WishlistItems).FirstOrDefault();
            }
        }

        //edit wishlist
        public bool AddWishlistItem(List<WishlistItem> WishlistItems)
        {
            using (eComContext context = new eComContext())
            {
                foreach (var wishItem in WishlistItems)
                {
                    context.WishlistItems.Add(wishItem);
                }
                return context.SaveChanges() > 0;
            }
        }
    }
}
