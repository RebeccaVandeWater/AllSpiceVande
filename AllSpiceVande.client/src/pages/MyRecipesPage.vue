<template>
  <div class="container-fluid">
    <section class="row pt-md-5" v-if="account.id">
      <div class="col-md-4 col-lg-3 col-xl-2 col-12 my-3" v-for="recipe in myRecipes" :key="recipe.id">
        <RecipeCard :recipeProp="recipe" />
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
import { computed } from 'vue';
import { AppState } from '../AppState.js';

export default {
  setup(){
    return {
      account: computed(() => AppState.account),
      myRecipes: computed(() => AppState.myRecipes)
    }
  }
}
</script>


<style lang="scss" scoped>
.create-btn{
  background-color: #527360;
  color: white;
  height: 6vh;
  width: 6vh;
  border-radius: 50%;
}
</style>