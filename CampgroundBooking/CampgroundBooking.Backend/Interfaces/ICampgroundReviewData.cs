using CampgroundBooking.Backend.Models.CampgroundModel;
using CampgroundBooking.Backend.Models;
using System;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundReviewData
    {
        Task<List<CampgroundReviewModel>> GetReviews(string CGName);
        Task<List<PersonReviewModel>> GetPersonReviews(string userEmail);
        Task<int> insertReview(int CGID, string email, string desc, int rating);
    }
}