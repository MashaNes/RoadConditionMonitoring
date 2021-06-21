import Vue from 'vue'
import VueRouter from 'vue-router'
import HomePage from "../pages/HomePage.vue"
import VehiclePage from "../pages/VehiclePage.vue"

Vue.use(VueRouter)

const routes = [
    {
        path: "/",
        name: "HomePage",
        component: HomePage
    },
    {
        path: "/vehicle-data/:id",
        name: "VehiclePage",
        component: VehiclePage,
        props: true
    }
]

const router = new VueRouter({
    mode: 'history',
    scrollBehavior (to, from, savedPosition) {
      return { x: 0, y: 0 };
    },
    base: process.env.BASE_URL,
    routes
  })
  
  export default router
