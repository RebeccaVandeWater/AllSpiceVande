using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _ingredientsRepository;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
  {
    _ingredientsRepository = ingredientsRepository;
    _recipesService = recipesService;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"INVALID ID: {ingredientId}");
    }
    return ingredient;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
  {
    int recipeId = ingredientData.RecipeId;

    Recipe recipe = _recipesService.GetRecipeById(recipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of the recipe: {recipe.Title}. You may not add ingredients to it.");
    }

    int ingredientId = _ingredientsRepository.CreateIngredient(ingredientData);

    Ingredient ingredient = GetIngredientById(ingredientId);

    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal void RemoveIngredient(int ingredientId, string userId)
  {
    Ingredient ingredientToRemove = GetIngredientById(ingredientId);

    Recipe recipe = _recipesService.GetRecipeById(ingredientToRemove.RecipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of {recipe.Title}. You may not remove an ingredient from it.");
    }

    _ingredientsRepository.RemoveIngredient(ingredientId);
  }
}
