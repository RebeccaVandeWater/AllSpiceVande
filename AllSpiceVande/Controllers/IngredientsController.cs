using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);

      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]

  public async Task<ActionResult<string>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      _ingredientsService.RemoveIngredient(ingredientId, userInfo.Id);

      return Ok("The ingredient was successfully removed.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
