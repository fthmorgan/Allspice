<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-12">
        <div class="bg-secondary d-flex justify-content-around rounded p-3">
          <button @click="filterBy = ''">All</button>
          <button @click="filterBy = 'favorites'">Favorites</button>
          <button @click="filterBy = 'myRecipes'">My Recipes</button>
        </div>
      </div>
    </div>
    <div class="row d-flex justify-content-center">
      <div class="col-3 card" v-for="r in recipes" :key="r.id">
        <RecipeCard :recipeProp="r" />
        <!-- <div>
          <h1>{{ r.title }}</h1>
          <p>{{ r.instructions }}</p>
          <img class="img-fluid" :src="r.img">
          <h2>{{ r.category }}</h2>
        </div>
        <div>
          Button trigger modal
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
            See More Details
          </button>

          Modal
          <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">{{ r.name }}</h5>
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                  <p>{{ r.title }}</p>
                  <img :src="r.img" class="img-fluid">
                  <p>{{ r.instructions }}</p>
                  <p>{{ r.category }}</p>
                  <p>Created At:{{ r.createdAt.toLocaleDateString() }}</p>
                  <p>Edited: {{ r.updatedAt.toLocaleDateString() }}</p>
                  <p>{{ r.creator.name }}</p>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Done</button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div>
          <button type="button" class="btn btn-primary" @click="addFavoriteRecipe(), setActiveRecipe()">Add To
            Favorites</button>
          <button @click="removeRecipe()" v-if="account.id == r.creatorId">Delete</button>
        </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { computed, onMounted, ref } from "vue";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const filterBy = ref('')

    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error(error.message)
      }
    }


    onMounted(() => {
      getRecipes()

    })
    return {
      filterBy,
      account: computed(() => AppState.account),
      recipes: computed(() => {
        if (filterBy.value == 'myRecipes') {
          return AppState.recipes.filter(r => r.creatorId == AppState.account.id)
        } else if (filterBy.value == 'favorites') {
          return AppState.recipes.filter(r => r == AppState.favoriteRecipes)
        } else {
          return AppState.recipes
        }
      }),

      async addFavoriteRecipe() {
        try {
          // logger.log('[ADDED TO FAVORITES]')

        } catch (error) {
          Pop.error(error.message)
        }
      },
    }
  }
}
</script>

<style scoped lang="scss">
  .home {
    display: grid;
    height: 80vh;
    place-content: center;
    text-align: center;
    user-select: none;

    .home-card {
      width: 50vw;

      >img {
        height: 200px;
        max-width: 200px;
        width: 100%;
        object-fit: contain;
        object-position: center;
      }
    }
  }
</style>
