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
    }
}
