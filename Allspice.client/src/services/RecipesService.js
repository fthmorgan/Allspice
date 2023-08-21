import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {


  async getRecipes() {
    const res = await api.get('api/recipes')
    logger.log('[GOT RECIPES]', res.data)

    const recipes = res.data.map(r => new Recipe(r))

    AppState.recipes = recipes
  }

  async removeRecipe(recipeId) {
    const res = await api.delete(`api/recipes/${recipeId}`)
    logger.log('[REMOVING RECIPE]', res.data)
    AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
  }

}

export const recipesService = new RecipesService()