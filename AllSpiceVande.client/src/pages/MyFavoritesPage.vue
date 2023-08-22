<template>
  <div class="container-fluid">
    <section class="row pt-md-5" v-if="account.id">
      <div class="col-md-4 col-lg-3 col-xl-2 col-12 my-3" v-for="favorite in myFavorites" :key="favorite.favoriteId">
        <RecipeCard :recipeProp="favorite" />
      </div>
    </section>
    <section class="row" v-else>
      <div class="col-12">
        <p class="fs-2">
          Loading... <i class="mdi mdi-loading mdi-spin"></i>
        </p>
      </div>
    </section>
  </div>
</template>


<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { favoritesService } from '../services/FavoritesService.js';

export default {
  setup(){
    async function getMyFavorites(){
      favoritesService.getMyFavorites()
    }

    onMounted(() => {
      getMyFavorites()
    })
    
    return {
      account: computed(() => AppState.account),
      myFavorites: computed(() => AppState.myFavorites)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>