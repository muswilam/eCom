using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using eCom.Entities;
using eCom.Web.Models;

namespace eCom.Web.ViewModels
{
    public class AddReviewViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required, MaxLength(500)]
        public string Body { get; set; }

        public DateTime ReviewedAt { get; set; }

        [Range(0, 5)]
        public byte Rate { get; set; }

        public int ProductId { get; set; }

    }

    public class ReviewsViewModel
    {
        public List<Review> Reviews { get; set; }

        public ApplicationUser AuthenticatedUser { get; set; }
        public ApplicationUserManager User { get; set; }

        public int ProductId { get; set; }
    }
}