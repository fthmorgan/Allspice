
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
    VALUES (@Title, @Instructions, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int recipeId = _db.ExecuteScalar<int>(sql, recipeData);

    return recipeId;
  }

  internal List<Recipe> GetRecipes()
  {
    string sql = @" 
    SELECT
    reci.*,
    acc.*
    FROM recipes reci
    JOIN accounts acc ON acc.id = reci.creatorId
    ;";

    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      }
    ).ToList();
    return recipes;
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


  internal Recipe UpdateRecipe(Recipe originalRecipe)
  {
    string sql = @" 
      UPDATE recipes
      SET
      title = @Title,
      category = @Category,
      instructions = @Instructions,
      img = @Img
      WHERE id = @Id
      Limit 1;
      SELECT * FROM recipes WHERE id = @Id
      ;";

    Recipe updatedRecipe = _db.QueryFirstOrDefault<Recipe>(sql, originalRecipe);

    return updatedRecipe;
  }
  internal void RemoveRecipes(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";

    _db.Execute(sql, new { recipeId });
  }
}
