<template>
  <form @submit.prevent="createRecipe()" class="row">
    <div class="mb-2 col-6">
      <label for="title">Title</label>
      <input v-model="editable.title" type="text" id="title" placeholder="Title..." title="Recipe Title" maxlength="255" minlength="1" required class="form-control">
    </div>
    <div class="col-6 mb-2">
      <label for="category">Category</label>
      <select v-model="editable.category" class="form-select" aria-label="Default select example" required>
        <option disabled selected>Category</option>
        <option v-for="category in categories" :key="category" :value="category">{{category}}</option>
      </select>
    </div>
    <div class="mb-2 col-12">
      <label for="subtitle">Subtitle</label>
      <input v-model="editable.subtitle" type="text" id="subtitle" placeholder="Details..." title="Subtitle" maxlength="255" class="form-control">
      <span class="fw-light ps-2">A brief description of the recipe</span>
    </div>
    <div class="mb-2 col-12">
      <label for="instructions">Instructions</label>
      <textarea v-model="editable.instructions" name="instructions" id="instructions" cols="30" rows="2" minlength="1" maxlength="700" required class="form-control"></textarea>
    </div>
    <!-- FIXME - Add a better way to preview or add images (file upload, unsplash search, etc.) -->
    <div class="mb-3 col-12">
      <label for="img">Recipe Image</label>
      <input v-model="editable.img" type="url" name="img" id="img" placeholder="Recipe Image..." required minlength="1" maxlength="700" class="form-control">
    </div>
    <div class="text-end">
      <button class="btn me-2" type="button" data-bs-dismiss="modal" aria-label="Close" @click="clearForm()">
        Cancel
      </button>
      <button class="btn btn-success">
        Submit
      </button>
    </div>
  </form>
</template>


<script>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap';

export default {
  setup(){
    const editable = ref({})
    return {
      editable,

      categories: ['Cheese', 'Italian', 'Specialty Coffee', 'Mexican', 'Soup', 'Beef', 'Chicken', 'Fish', 'Indian', 'Chinese', 'Japanese', 'Korean', 'American', 'Vegetarian', 'Vegan', 'Misc'],

      async clearForm(){
        const clearConfirm = await Pop.confirm('Are you sure you want to cancel?');

        if(!clearConfirm){
          return;
        }

        editable.value = {};
      },

      async createRecipe(){
        try {
          const recipeData = editable.value;

          recipesService.createRecipe(recipeData);

          Modal.getOrCreateInstance('#createRecipeModal').hide()

          editable.value = {};
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

</style>