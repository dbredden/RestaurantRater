using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // Create new ratings
        // Post api/rating
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            // Check if model is null
            if (model is null)
                return BadRequest("Your request body cannot be empty.");

            // Check if ModelState is Invalid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the Restaurant by the model.RestaurantId and see if it exists
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist");

            // Create the Rating

            // Add to the rating table
            //_context.Ratings.Add(model);

            // Add to the Restaurant Entity
            restaurant.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {model.RestaurantId} successfully!");

            return InternalServerError();
        }

        // Get a rating by ID

        // Get all ratings

        // Get all ratings for a restaurant by restaurant id

        // Update a rating

        // Delete a rating
    }
}
