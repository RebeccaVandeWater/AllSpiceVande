using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpiceVande.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StepsController : ControllerBase
{
  private readonly StepsService _stepsService;
  private readonly Auth0Provider _auth0Provider;

  public StepsController(StepsService stepsService, Auth0Provider auth0Provider)
  {
    _stepsService = stepsService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]

  public async Task<ActionResult<Step>> CreateStep([FromBody] Step stepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Step step = _stepsService.CreateStep(stepData, userInfo.Id);

      return Ok(step);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{stepId}")]

  public async Task<ActionResult<Step>> EditStep([FromBody] Step stepData, int stepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Step step = _stepsService.EditStep(stepData, stepId, userInfo.Id);

      return Ok(step);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{stepId}")]

  public async Task<ActionResult<Step>> RemoveStep(int stepId)
  {
    Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

    _stepsService.RemoveStep(stepId);

    return Ok("This step was successfully removed.");
  }
}
