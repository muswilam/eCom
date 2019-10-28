using eCom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCom.Data;

namespace eCom.Services
{
    public class ReviewService
    {
        #region Singleton Non-Thread Safety 
        public static ReviewService Instance
        {
            get
            {
                if (instance == null) instance = new ReviewService();

                return instance;
            }
        }
        private static ReviewService instance { get; set; }
        private ReviewService()
        {
        }
        #endregion

        //get reviews by productId
        public List<Review> GetReviews(int productId)
        {
            using (var context = new eComContext())
            {
                return context.Reviews.Where(r => r.ProductId == productId).ToList();
            }
        }

        //create a new review 
        public bool SaveReview(Review newReview)
        {
            using (var context = new eComContext())
            {
                context.Reviews.Add(newReview);

                return context.SaveChanges() > 0;
            }
        }

        //delete review
        public void RemoveReview(int id)
        {
            using (var context = new eComContext())
            {
                var review = context.Reviews.Where(r => r.Id == id).First();

                context.Reviews.Remove(review);
                context.SaveChanges();
            }
        }

        //get avarage of rates of specific product
        public int GetRatesAvarage(int productId)
        {
            using (var context = new eComContext())
            {
                var reviews = context.Reviews.Where(r => r.ProductId == productId);

                var rates = reviews.Select(r => r.Rate);

                var multipliedRatesCount = (rates.Where(r => r == 5).Count() * 5 ) + 
                    (rates.Where(r => r == 4).Count()*4) + 
                    (rates.Where(r => r == 3).Count()*3) + 
                    (rates.Where(r => r == 2).Count()*2) +
                    (rates.Where(r => r == 1).Count()*1);

                return multipliedRatesCount / rates.Count();
            }
        }
    }
}
