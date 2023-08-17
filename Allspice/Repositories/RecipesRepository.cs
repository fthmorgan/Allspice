
namespace Allspice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateRecipe(Recipe recipeData)
  {
    string sql = @" 
    INSERT INTO recipes (title, instructions, img, category, creatorId)
    VALUES (@Title, @Instruction, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int recipeId = _db.ExecuteScalar<int>(sql, recipeData);

    return recipeId;
  }

  internal Recipe GetRecipesById(int recipeId)
  {
    string sql = @" 
        SELECT
        reci.*,
        acc.*
        FROM recipes reci
        JOIN accounts acc ON acc.id = reci.creatorId
        WHERE reci.id = @recipeId
        ;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
new { recipeId }).FirstOrDefault();
    return recipe;
  }
}
