using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
  private readonly RatingsService _ratingsService;

  private readonly Auth0Provider _auth0Provider;
  public RatingsController(RatingsService ratingsService, Auth0Provider auth0Provider)
  {
    _ratingsService = ratingsService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  public async Task<ActionResult<Rating>> CreateRating([FromBody] Rating ratingData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      ratingData.AccountId = userInfo.Id;

      Rating rating = _ratingsService.CreateRating(ratingData);

      return Ok(rating);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{ratingId}")]

  public async Task<ActionResult<Rating>> EditRating([FromBody] Rating ratingData, int ratingId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Rating rating = _ratingsService.EditRating(ratingId, userInfo.Id, ratingData);

      return Ok(rating);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ratingId}")]

  public async Task<ActionResult<string>> RemoveRating(int ratingId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      _ratingsService.RemoveRating(ratingId, userInfo.Id);

      return Ok("Rating was successfully removed.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
