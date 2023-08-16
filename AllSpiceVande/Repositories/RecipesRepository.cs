using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Repositories;

public class RecipesRepository
{

  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes (title, instructions, img, category, creatorId)
    VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int recipeId = _db.ExecuteScalar<int>(sql, recipeData);

    return recipeId;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
        SELECT
        rec.*,
        acc.*
        FROM recipes rec 
        JOIN accounts acc ON acc.id = rec.creatorId
        WHERE rec.id = @recipeId
        ;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
      new { recipeId }).FirstOrDefault();
    return recipe;
  }
}
