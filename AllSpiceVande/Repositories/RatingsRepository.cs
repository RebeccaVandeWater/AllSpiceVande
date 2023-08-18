using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class RatingsRepository
{
  private readonly IDbConnection _db;

  public RatingsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateRating(Rating ratingData)
  {
    string sql = @"
        INSERT INTO ratings(score, accountId, recipeId)
        VALUES(@Score, @AccountId, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";

    int ratingId = _db.ExecuteScalar<int>(sql, ratingData);

    return ratingId;
  }

  internal Rating EditRating(Rating originalRating)
  {
    string sql = @"
    UPDATE ratings 
    SET score = @Score 
    WHERE id = @Id;
    SELECT * FROM ratings WHERE id = @Id
    ;";

    Rating rating = _db.QueryFirstOrDefault<Rating>(sql, originalRating);

    return rating;
  }

  internal Rating GetRatingById(int ratingId)
  {
    string sql = "SELECT * FROM ratings WHERE id = @ratingId;";

    Rating rating = _db.QueryFirstOrDefault<Rating>(sql, new { ratingId });

    return rating;
  }

  internal List<Rating> GetRatingsByRecipeId(int recipeId)
  {
    string sql = "SELECT * FROM ratings WHERE recipeId = @recipeId;";

    List<Rating> ratings = _db.Query<Rating>(sql, new { recipeId }).ToList();

    return ratings;
  }

  internal void RemoveRating(int ratingId)
  {
    string sql = "DELETE FROM ratings WHERE id = @ratingId LIMIT 1;";

    _db.Execute(sql, new { ratingId });
  }
}
