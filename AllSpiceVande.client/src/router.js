import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    children: [
      {
        path: '/recipes',
        name: 'Recipes',
        component: loadPage('RecipePage')
      },
      {
        path: '/myrecipes',
        name: 'My Recipes',
        component: loadPage('MyRecipesPage')
      },
      {
        path: '/myfavorites',
        name: 'My Favorites',
        component: loadPage('MyFavoritesPage')
      },
    ]
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
