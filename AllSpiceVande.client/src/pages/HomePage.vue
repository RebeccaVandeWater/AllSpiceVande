<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 home-img d-md-flex d-none text-center align-items-center justify-content-center">
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
      <div class="col-9 d-md-flex d-none justify-content-center">
        <div class="btn-banner p-1 px-3 rounded" v-if="account.id">
          <router-link :to="{name: 'Recipes'}">
            <button class="btn home-btn pe-4" title="Home">
              Home
            </button>
          </router-link>
          <router-link :to="{name: 'My Recipes'}">
            <button class="btn home-btn pe-4" title="My Recipes">
              My Recipes
            </button>
          </router-link>
          <router-link :to="{name: 'My Favorites'}">
            <button class="btn home-btn" title="Favorites">
              My Favorites
            </button>
          </router-link>
          <button class="btn home-btn" id="createTab" title="Create Recipe">
            Create Recipe
          </button>
        </div>
      </div>
      <div class="dropdown-center d-md-none mt-3">
        <button class="btn btn-light dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          Filter Recipes
        </button>
        <ul class="dropdown-menu ms-2">
          <li>
            <router-link :to="{name: 'Recipes'}">
              <button class="btn home-btn pe-4" title="Home">
                Home
              </button>
            </router-link>
          </li>
          <li>
            <router-link :to="{name: 'My Recipes'}">
              <button class="btn home-btn pe-4" title="My Recipes">
                My Recipes
              </button>
            </router-link>
          </li>
          <li>
            <router-link :to="{name: 'My Favorites'}">
              <button class="btn home-btn" title="Favorites">
                My Favorites
              </button>
            </router-link>
          </li>
          <li>
            <button class="btn home-btn" id="createTab" title="Create Recipe">
            Create Recipe
            </button>
          </li>
        </ul>
      </div>
    </section>
    <section class="row">
      <router-view>
      </router-view>
    </section>
    <section class="row">
      <div class="col-12 d-md-flex align-items-end justify-content-end p-3 d-none" v-if="account.id">
        <button class="btn create-btn fs-2 text-center d-flex align-items-center justify-content-center" data-bs-toggle="modal" data-bs-target="#createRecipeModal" title="Create Recipe">
          <i class="mdi mdi-plus"></i>
        </button>
      </div>
    </section>
  </div>
  <ModalComponent id="createRecipeModal">
    <template #modalHeader>
      <section class="row">
        <div class="modal-banner-style col-12">
          <span class="fw-bold fs-4">
            New Recipe
          </span>
        </div>
      </section>
    </template>
    <template #modalBody>
      <CreateRecipeModal/>
    </template>
  </ModalComponent>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import {recipesService} from '../services/RecipesService.js'
import ModalComponent from '../components/ModalComponent.vue';
import { useRoute } from 'vue-router';
import { router } from '../router.js';

export default {
    setup() {
      const route = useRoute()
      async function getRecipes() {
          try {
              await recipesService.getRecipes();
          }
          catch (error) {
              Pop.error(error.message);
          }
      }

      function openRecipesPage(){
        if(route.name == 'Home'){
          router.push({name: 'Recipes'})
        }
      }

      onMounted(() => {
          getRecipes();
          openRecipesPage();
      });

      return {
          account: computed(() => AppState.account),
          recipes: computed(() => AppState.recipes)
      };
    },
    components: { ModalComponent }
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
  position: fixed;
  bottom: 3vh;
}

.modal-banner-style{
  // background-color: #527360;
  font-family: 'Playfair Display', serif;
  color: black;
}

.home-btn:hover{
  color: #2bbe6b;
  text-decoration: underline;
}

.btn-banner{
  background-color: whitesmoke;
  box-shadow: -2px 9px 20px -11px rgba(0,0,0,0.63);
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
    position: absolute;
    top: 45vh;
  }

  #createTab{
    display: none;
  }
}
</style>
