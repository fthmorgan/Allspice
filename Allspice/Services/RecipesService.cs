
namespace Allspice.Services;

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
  internal List<Recipe> GetRecipes()
  {
    List<Recipe> recipes = _recipesRepository.GetRecipes();
    return recipes;
  }
  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipesRepository.GetRecipesById(recipeId);

    if (recipe == null)
    {
      throw new Exception("No recipe found");
    }
    return recipe;
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);

    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;

    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;

    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;

    Recipe recipe = _recipesRepository.UpdateRecipe(originalRecipe);

    Recipe updatedRecipe = GetRecipeById(recipeId);

    return updatedRecipe;
  }

  internal void RemoveRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception("You cannot delete a photo that is not yours!");
    }
    _recipesRepository.RemoveRecipes(recipeId);
  }
}
