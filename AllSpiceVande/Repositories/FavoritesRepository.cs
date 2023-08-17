using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
        INSERT INTO favorites(accountId, recipeId)
        VALUES(@AccountId, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";

    int favoriteId = _db.ExecuteScalar<int>(sql, favoriteData);

    return favoriteId;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";

    Favorite favorite = _db.QueryFirstOrDefault<Favorite>(sql, new { favoriteId });

    return favorite;
  }
}
