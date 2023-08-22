import { AppState } from "../AppState.js";
import { Favorite } from "../models/Favorite.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async getMyFavorites() {
    try {
      const res = await api.get('account/favorites')

      logger.log('[GOT ACCOUNT FAVORITES]', res.data)

      const favorites = res.data.map(p => new Favorite(p))

      AppState.myFavorites = favorites

      logger.log(favorites)
    }
    catch (error) {
      Pop.error(error.message);
    }
  }

  async createFavorite(recipeData) {
    const res = await api.post('api/favorites', recipeData)

    logger.log('[CREATED FAVORITE]', res.data)

    const favorite = { favoriteId: res.data.id, id: res.data.recipeId }

    AppState.myFavorites.push(favorite)
  }

  async removeFavorite(favorite) {
    const res = await api.delete(`api/favorites/${favorite.favoriteId}`)

    logger.log('[REMOVED FAVORITE]', res.data)
    const favoriteToRemove = AppState.myFavorites.findIndex(f => f.favoriteId == favorite.favoriteId)

    AppState.myFavorites.splice(favoriteToRemove, 1)
  }
}

export const favoritesService = new FavoritesService()