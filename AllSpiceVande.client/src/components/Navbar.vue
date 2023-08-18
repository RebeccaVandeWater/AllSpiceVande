<template>
  <nav class="navbar-nav bg-body-tertiary">
    <div class="container-fluid">
      <section class="row flex-grow-1">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <router-link :to="{name: 'Home'}">
            <div>
              <a class="navbar-brand fs-3 text-dark"><i class="mdi mdi-home" title="Home"></i></a>
            </div>
          </router-link>
          <div class="d-flex align-items-center">
            <form role="search">
              <div class="input-group">
                <input class="form-control" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-success" type="submit"><i class="mdi mdi-magnify" title="Search"></i></button>
              </div>
            </form>
            <span class="ps-4 navbar-text">
              <Login />
            </span>
          </div>
        </div>
      </section>
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue';
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
