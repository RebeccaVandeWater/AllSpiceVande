using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpiceVande.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);

    if (favorite == null)
    {
      throw new Exception($"INVALID ID: {favoriteId}");
    }

    return favorite;
  }
  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    int favoriteId = _favoritesRepository.CreateFavorite(favoriteData);

    Favorite favorite = GetFavoriteById(favoriteId);

    return favorite;
  }
}
