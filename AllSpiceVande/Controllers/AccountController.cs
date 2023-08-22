namespace AllSpiceVande.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;
  private readonly RecipesService _recipesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService, RecipesService recipesService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
    _recipesService = recipesService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]

  public async Task<ActionResult<List<RecipeFavorite>>> GetMyFavorites()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      List<RecipeFavorite> favorites = _favoritesService.GetMyFavorites(userInfo.Id);

      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("recipes")]

  public async Task<ActionResult<List<Recipe>>> GetMyRecipes()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      List<Recipe> recipes = _recipesService.GetMyRecipes(userInfo.Id);

      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{userEmail}")]

  public async Task<ActionResult<Account>> Edit([FromBody] Account accountData, string userEmail)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Account account = _accountService.Edit(accountData, userEmail);

      return Ok(account);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
