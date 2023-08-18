using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
  private readonly LikesService _likesService;
  private readonly Auth0Provider _auth0Provider;

  public LikesController(LikesService likesService, Auth0Provider auth0Provider)
  {
    _likesService = likesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Like>> CreateLike([FromBody] Like likeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      likeData.AccountId = userInfo.Id;

      Like like = _likesService.CreateLike(likeData);

      return Ok(like);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{likeId}")]

  public async Task<ActionResult<string>> RemoveLike(int likeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      _likesService.RemoveLike(likeId, userInfo.Id);

      return Ok("The like was successfully removed.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
