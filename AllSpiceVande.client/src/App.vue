<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
@import url('https://fonts.googleapis.com/css2?family=Black+Ops+One&family=Cinzel+Decorative&family=Concert+One&family=Forum&family=Indie+Flower&family=Kalam:wght@300&family=Kumar+One+Outline&family=Playfair+Display:ital,wght@0,400;0,500;0,700;0,800;1,400;1,600;1,800&family=Shadows+Into+Light&family=Shrikhand&family=Staatliches&display=swap');

:root{
  --main-height: calc(100vh - 32px - 64px);
}

.subtitle-font{
  font-family: 'Playfair Display', serif;
  color: white;
  text-shadow: 2px 2px 3px black;
}

.title-font{
  font-family: 'Playfair Display', serif;
  font-weight: 600;
}
</style>
