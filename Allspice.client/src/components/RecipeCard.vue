<template>
  <div>
    <h1>{{ recipeProp.title }}</h1>
    <p>{{ recipeProp.instructions }}</p>
    <img class="img-fluid" :src="recipeProp.img">
    <h2>{{ recipeProp.category }}</h2>
  </div>
  <div>
    <!-- Button trigger modal -->
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
      See More Details
    </button>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">{{ recipeProp.name }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <p>{{ recipeProp.title }}</p>
            <img :src="recipeProp.img" class="img-fluid">
            <p>{{ recipeProp.instructions }}</p>
            <p>{{ recipeProp.category }}</p>
            <p>Created At:{{ recipeProp.createdAt.toLocaleDateString() }}</p>
            <p>Edited: {{ recipeProp.updatedAt.toLocaleDateString() }}</p>
            <p>{{ recipeProp.creator.name }}</p>
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
    <button @click="removeRecipe()" v-if="account.id == recipeProp.creatorId">Delete</button>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { recipesService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    recipeProp: { type: Object, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),

      async removeRecipe() {
        try {
          await recipesService.removeRecipe(props.recipeProp.id)
        } catch (error) {
          Pop.error(error.message)
        }
      },


    }
  }
}
</script>


<style lang="scss" scoped></style>