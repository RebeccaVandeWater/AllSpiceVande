using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      favoriteData.AccountId = userInfo.Id;

      Favorite favorite = _favoritesService.CreateFavorite(favoriteData);

      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
