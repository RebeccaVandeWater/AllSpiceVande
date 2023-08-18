using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class RatingsService
{
  private readonly RatingsRepository _ratingsRepository;

  public RatingsService(RatingsRepository ratingsRepository)
  {
    _ratingsRepository = ratingsRepository;
  }

  internal Rating GetRatingById(int ratingId)
  {
    Rating rating = _ratingsRepository.GetRatingById(ratingId);

    return rating;
  }

  internal List<Rating> GetRatingsByRecipeId(int recipeId)
  {
    List<Rating> ratings = _ratingsRepository.GetRatingsByRecipeId(recipeId);

    return ratings;
  }
  internal Rating CreateRating(Rating ratingData)
  {
    List<Rating> recipeRatings = GetRatingsByRecipeId(ratingData.RecipeId);

    Rating userRating = recipeRatings.Find(r => r.AccountId == ratingData.AccountId);

    if (userRating != null)
    {
      throw new Exception("You have already rated this recipe. You cannot rate a recipe twice.");
    }

    int ratingId = _ratingsRepository.CreateRating(ratingData);

    Rating rating = GetRatingById(ratingId);

    return rating;
  }

  internal void RemoveRating(int ratingId, string userId)
  {
    Rating ratingToRemove = GetRatingById(ratingId);

    if (ratingToRemove.AccountId != userId)
    {
      throw new Exception("You are not the creator of this rating. You cannot remove a rating you did not create.");
    }

    _ratingsRepository.RemoveRating(ratingId);
  }

  internal Rating EditRating(int ratingId, string userId, Rating ratingData)
  {
    Rating originalRating = GetRatingById(ratingId);

    if (originalRating.AccountId != userId)
    {
      throw new Exception("You are not the creator of this rating. You cannot edit a rating you did not create.");
    }

    originalRating.Score = ratingData.Score ?? originalRating.Score;

    Rating editedRating = _ratingsRepository.EditRating(originalRating);

    Rating rating = GetRatingById(editedRating.Id);

    return rating;
  }
}
