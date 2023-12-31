import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js"

class RecipesService {
  async createRecipe(recipeData) {
    const res = await api.post('api/recipes', recipeData)

    logger.log('[CREATED RECIPE]', res.data)

    AppState.recipes.push(new Recipe(res.data))

    AppState.myRecipes.push(new Recipe(res.data))
  }

  async getMyRecipes() {
    try {
      const res = await api.get('account/recipes')

      logger.log('[GOT ACCOUNT RECIPES]', res.data)

      const recipes = res.data.map(p => new Recipe(p))

      AppState.myRecipes = recipes
    }
    catch (error) {
      Pop.error(error.message);
    }
  }
  async getRecipes() {
    const res = await api.get('api/recipes')

    logger.log('[GOT RECIPES]', res.data)

    const recipes = res.data.map(p => new Recipe(p))

    AppState.recipes = recipes
  }
}

export const recipesService = new RecipesService()