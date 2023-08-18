using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class RecipesService
{
  private readonly RecipesRepository _recipesRepository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    int recipeId = _recipesRepository.CreateRecipe(recipeData);

    Recipe recipe = GetRecipeById(recipeId);

    return recipe;
  }

  internal Recipe EditRecipe(int recipeId, Recipe recipeData, string userId)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);

    if (originalRecipe.CreatorId != userId)
    {
      throw new Exception($"You are not the creator of {originalRecipe.Title}. You may not edit it.");
    }

    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

    Recipe editedRecipe = _recipesRepository.EditRecipe(originalRecipe);

    Recipe recipe = GetRecipeById(editedRecipe.Id);

    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _recipesRepository.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"{recipeId} is not a valid ID.");
    }

    return recipe;
  }

  internal Recipe RemoveRecipe(int recipeId, string userId)
  {
    Recipe recipeToRemove = GetRecipeById(recipeId);

    if (recipeToRemove.CreatorId != userId)
    {
      throw new Exception($"You are not the owner of the recipe with the title {recipeToRemove.Title}. You may not remove it.");
    }

    _recipesRepository.RemoveRecipe(recipeId);

    return recipeToRemove;
  }
}
