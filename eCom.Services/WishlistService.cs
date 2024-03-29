﻿using System;
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

        //is product has been wished before
        public bool IsProductWished(string userId, int productId)
        {
            using (eComContext context = new eComContext())
            {
                var exist = false;
          
                var userWishlist = context.Wishlists.Where(w => w.UserId == userId).Include(w => w.WishlistItems).Include("WishlistItems.Product").FirstOrDefault();

                if(userWishlist != null && userWishlist.WishlistItems != null &&userWishlist.WishlistItems.Count > 0)
                {
                    foreach (var wishItem in userWishlist.WishlistItems)
                    {
                        if (wishItem.ProductId == productId)
                            exist = true;
                    }
                }

                return exist;
            }
        }

        //get wishlist by userId
        public Wishlist GetWishlist(string userId)
        {
            using (eComContext context = new eComContext())
            {
                return context.Wishlists.Where(w => w.UserId == userId).Include(w => w.WishlistItems).Include("WishlistItems.Product").FirstOrDefault();
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

        //remove product from wishlist
        public bool RemoveItemFromWishlist(string userId, int productId)
        {
            using (eComContext context = new eComContext())
            {
                var wishlistItems = context.WishlistItems.Where(wi => wi.Wishlist.UserId == userId).ToList();

                foreach (var wishItem in wishlistItems)
                {
                    if(wishItem.ProductId == productId)
                    {
                        context.WishlistItems.Remove(wishItem);
                        return context.SaveChanges() > 0;
                    }
                }
                return false;
            }
        }

        //Empty wishlistItems by userId
        public bool EmptyWishlistItems(string userId)
        {
            using (eComContext context = new eComContext())
            {
                var wishlist = context.Wishlists.Where(w => w.UserId == userId).Include(w => w.WishlistItems).FirstOrDefault();
             
                context.WishlistItems.RemoveRange(wishlist.WishlistItems);
                return context.SaveChanges() > 0;
            }
        }
    }
}
