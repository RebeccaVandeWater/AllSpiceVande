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

  internal Recipe EditRecipe(Recipe originalRecipe)
  {
    string sql = @"
        UPDATE recipes 
        SET 
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        WHERE id = @Id;
        SELECT * FROM recipes WHERE id = @Id
        ;";

    Recipe recipe = _db.QueryFirstOrDefault<Recipe>(sql, originalRecipe);

    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT 
    rec.*,
    acc.*
    FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId    
    ;";

    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>
    (
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      }
    ).ToList();

    return recipes;
  }

  internal List<Recipe> GetMyRecipes(string userId)
  {
    string sql = @"
    SELECT 
    rec.*,
    acc.* 
    FROM recipes rec
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE creatorId = @userId
    ;";

    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
      new { userId }).ToList();

    return recipes;
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

  internal void RemoveRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1";

    _db.Execute(sql, new { recipeId });
  }
}
