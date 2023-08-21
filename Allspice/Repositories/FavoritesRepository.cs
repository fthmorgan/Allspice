
namespace Allspice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @" 
      INSERT INTO favorites (recipeId, accountId)
      VALUES (@RecipeId, @AccountId);
      SELECT LAST_INSERT_ID()
      ;";

    int favoriteId = _db.ExecuteScalar<int>(sql, favoriteData);

    favoriteData.Id = favoriteId;
    return favoriteData;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @" 
      SELECT * FROM favorites WHERE id = @favoriteId;";
    Favorite favorite = _db.QueryFirstOrDefault<Favorite>(sql, new { favoriteId });
    return favorite;
  }

  internal List<RecipeFavorites> GetMyRecipeFavorites(string userId)
  {
    string sql = @" 
    SELECT
    fav.*,
    rec.*,
    acc.*
    FROM favorites fav
    JOIN recipes rec ON rec.id = fav.recipeId
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE fav.accountId = @userId
    ;";

    List<RecipeFavorites> favorites = _db.Query<Favorite, RecipeFavorites, Profile, RecipeFavorites>(
      sql,
      (favorite, recipe, profile) =>
      {
        recipe.FavoriteId = favorite.Id;
        recipe.Creator = profile;
        return recipe;
      },
      new { userId }
    ).ToList();

    return favorites;
  }

  internal void RemoveFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1";
    int rowsAffected = _db.Execute(sql, new { favoriteId });
  }
}
