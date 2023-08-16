
namespace Allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  Auth0Provider = _auth0Provider;


public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HTTPPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] RecipesController recipesData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>
    }
    catch (System.Exception)
    {

      throw;
    }
  }






}


