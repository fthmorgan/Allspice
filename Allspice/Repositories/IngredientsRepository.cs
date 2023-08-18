
namespace Allspice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateIngredient(Ingredient ingredientData)
  {
    string sql = @" 
    INSERT INTO ingredients (name, quantity, recipeId, creatorId)
    VALUES (@Name, @Quantity, @RecipeId, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int ingredientId = _db.ExecuteScalar<int>(sql, ingredientData);
    return ingredientId;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @" 
        SELECT
        ing.*,
        acc.*
        FROM ingredients ing
        JOIN accounts acc ON acc.id = ing.creatorId
        WHERE ing.id = @ingredientId
        ;";

    Ingredient ingredient = _db.Query<Ingredient, Profile, Ingredient>(
      sql,
      (ingredient, profile) =>
      {
        ingredient.CreatorId = profile.Id;
        return ingredient;
      },
      new { ingredientId }).FirstOrDefault();
    return ingredient;
  }
}
