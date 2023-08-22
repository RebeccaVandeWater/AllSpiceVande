<template>
  <div class="d-flex flex-column flex-grow-1 justify-content-between recipe-img" title="View Recipe" role="button">
    <div class="d-flex justify-content-between">
      <p class="p-1 m-2 grey-box-category">
        {{ recipeProp.category }}
      </p>
      <div v-if="account.id">
        <div class="grey-box-btn me-3"  v-if="favorited">
          <button class="btn" @click="removeFavorite()"  title="Unfavorite Recipe">
            <i class="mdi mdi-heart text-danger"></i>
          </button>
        </div>
        <div class="grey-box-btn me-3" v-else>
          <button class="btn d-flex align-items-center" @click="createFavorite(recipeProp.id)" title="Favorite Recipe">
            <i class="mdi mdi-heart-outline"></i>
          </button>
        </div>
      </div>
    </div>
    <div class="p-2 m-2 grey-box-title">
      <p class="m-0">
        <span>
          {{ recipeProp.title }}
        </span> <br>
        <span>
          {{ recipeProp.subtitle }}
        </span>
      </p>
    </div>
  </div>

</template>


<script>
import { computed } from 'vue';
import { Recipe } from '../models/Recipe.js';
import { AppState } from '../AppState.js';
import { favoritesService } from '../services/FavoritesService.js';
import Pop from '../utils/Pop.js';


export default {
  props: {
    recipeProp: {type: Recipe, required: true}
  },

  setup(props){
    return {
      account: computed(() => AppState.account),
      recipeImg: computed(() => `url(${props.recipeProp.img})`),
      myFavorites: computed(() => AppState.myFavorites),
      favorited: computed(() => {
        let foundFavorite = AppState.myFavorites?.find(f => f.id == props.recipeProp.id)

        return foundFavorite;
      }),

      async createFavorite(recipeId){
        try {
          const recipeData = {recipeId: recipeId};
          await favoritesService.createFavorite(recipeData);
        }
        catch (error) {
          Pop.error(error.message);
        }
      },

      async removeFavorite(){
        try {
          let favorite = AppState.myFavorites.find(f => f.id == props.recipeProp.id)
          await favoritesService.removeFavorite(favorite);
        }
        catch (error) {
          Pop.error(error.message);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.grey-box-category{
  background: rgba(187, 187, 187, 0.69);
  border-radius: 16px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(6.8px);
  -webkit-backdrop-filter: blur(6.8px);
  border: 1px solid rgba(187, 187, 187, 0.3);
  width: fit-content;
}
.grey-box-title{
  background: rgba(187, 187, 187, 0.69);
  border-radius: 7px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(6.8px);
  -webkit-backdrop-filter: blur(6.8px);
  border: 1px solid rgba(187, 187, 187, 0.3);
}
.grey-box-btn{
  background: rgba(187, 187, 187, 0.69);
  border-bottom-right-radius: 7px;
  border-bottom-left-radius: 7px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(6.8px);
  -webkit-backdrop-filter: blur(6.8px);
  border: 1px solid rgba(187, 187, 187, 0.3);
  width: fit-content;
  height: fit-content;
}

.recipe-img{
  background-image: v-bind(recipeImg);
  background-position: center;
  background-size: cover;
  height: 40vh;
  border-radius: 5%;
}

@media(min-width: 768px)
{
  .recipe-img{
  background-image: v-bind(recipeImg);
  background-position: center;
  background-size: cover;
  height: 30vh;
  border-radius: 5%;
}
}
</style>
