using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly LikesService _likesService;

  public CommentsController(CommentsService commentsService, Auth0Provider auth0Provider, LikesService likesService)
  {
    _commentsService = commentsService;
    _auth0Provider = auth0Provider;
    _likesService = likesService;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      commentData.CreatorId = userInfo.Id;

      Comment comment = _commentsService.CreateComment(commentData);

      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{commentId}")]

  public async Task<ActionResult<Comment>> EditComment([FromBody] Comment commentData, int commentId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Comment comment = _commentsService.EditComment(commentData, userInfo.Id, commentId);

      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{commentId}")]

  public async Task<ActionResult<string>> RemoveComment(int commentId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      _commentsService.RemoveComment(commentId, userInfo.Id);

      return Ok("The comment was successfully removed.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{commentId}/likes")]

  public ActionResult<List<Like>> GetCommentLikes(int commentId)
  {
    try
    {
      List<Like> likes = _likesService.GetCommentLikes(commentId);

      return Ok(likes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
