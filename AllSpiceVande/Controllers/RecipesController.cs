using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;
  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
    _ingredientsService = ingredientsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeId}")]

  public async Task<ActionResult<Recipe>> EditRecipe([FromBody] Recipe recipeData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Recipe recipe = _recipesService.EditRecipe(recipeId, recipeData, userInfo.Id);

      return recipe;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]

  public async Task<ActionResult<string>> RemoveRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Recipe recipe = _recipesService.RemoveRecipe(recipeId, userInfo.Id);

      return Ok($"The recipe with the title {recipe.Title} was successfully deleted.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]

  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetAllRecipes();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]

  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);

      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
