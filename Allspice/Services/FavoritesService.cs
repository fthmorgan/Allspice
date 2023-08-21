
namespace Allspice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _favoritesRepository.CreateFavorite(favoriteData);
    return favorite;
  }

  internal List<RecipeFavorites> GetMyRecipeFavorites(string userId)
  {
    List<RecipeFavorites> favorites = _favoritesRepository.GetMyRecipeFavorites(userId);
    return favorites;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception($"BAD ID");
    }
    return favorite;
  }

  internal void RemoveFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);

    if (favorite.AccountId != userId)
    {
      throw new Exception("NOT YOUR DATA");
    }
    _favoritesRepository.RemoveFavorite(favoriteId);
  }
}
