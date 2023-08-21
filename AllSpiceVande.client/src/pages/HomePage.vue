<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 home-img d-flex text-center align-items-center justify-content-center">
        <div>
          <h1 class="subtitle-font">
            All-Spice
          </h1>
          <p class="subtitle-font">
            Cherish Your Family and Their Cooking
          </p>
        </div>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-3 d-flex justify-content-center">
        <div class="btn-banner p-1 px-3 rounded" v-if="account.id">
          <button class="btn home-btn pe-4" title="Home">
            Home
          </button>
          <button class="btn home-btn pe-4" title="My Recipes">
            My Recipes
          </button>
          <button class="btn home-btn" title="Favorites">
            Favorites
          </button>
        </div>
      </div>
    </section>
    <section class="row pt-md-5 justify-content-between" v-if="account.id">
      <div class="col-md-4 col-12 my-3" v-for="recipe in recipes" :key="recipe.id">
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
    <section class="row">
      <div class="col-12 position-fixed d-flex align-items-end justify-content-end p-3" v-if="account.id">
        <button class="btn create-btn fs-2 text-center d-flex align-items-center justify-content-center" title="Create Recipe">
          <i class="mdi mdi-plus"></i>
        </button>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import {recipesService} from '../services/RecipesService.js'
import RecipeCard from '../components/RecipeCard.vue';

export default {
    setup() {
        async function getRecipes() {
          try {
            await recipesService.getRecipes();
          }
          catch (error) {
            Pop.error(error.message);
          }
        }

        onMounted(() => {
            getRecipes();
        });
        return {
            account: computed(() => AppState.account),
            recipes: computed(() => AppState.recipes)
        };
    },
    components: { RecipeCard }
}
</script>

<style scoped lang="scss">
.home-btn{
  color: #1c7f47;
  font-family: 'Playfair Display', serif;
  font-weight: 500;
}
.create-btn{
  background-color: #527360;
  color: white;
  height: 6vh;
  width: 6vh;
  border-radius: 50%;
}

.home-btn:hover{
  color: #2bbe6b;
  text-decoration: underline;
}

.btn-banner{
  background-color: whitesmoke;
  width: fit-content;
  box-shadow: -2px 9px 20px -11px rgba(0,0,0,0.63);
  position: absolute;
  top: 39vh;
  right: auto;
}
.home-img{
  background-image: url(https://images.unsplash.com/photo-1509440159596-0249088772ff?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1172&q=80);
  background-position: center;
  background-size:cover;
  height: 40vh;
}

@media(min-width: 768px)
{
  .btn-banner{
    background-color: whitesmoke;
    width: fit-content;
    box-shadow: -2px 9px 20px -11px rgba(0,0,0,0.63);
    top: 45vh;
  }
}
</style>
