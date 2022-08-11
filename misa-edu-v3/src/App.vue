<template>
  <div id="app" :class="{ 'menu-no-expand': this.toggleMenu }">
    <the-menu @toggleMenu="handleToggleMenu" />
    <div class="main">
      <the-header :userName="userName" />
      <the-content />
    </div>
  </div>
</template>

<script>
import TheContent from "./components/layout/TheContent.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheMenu from "./components/layout/TheMenu.vue";

export default {
  name: "App",
  data() {
    return {
      userName: {
        name: "admin",
        position: "Quản trị hệ thống",
      },
      toggleMenu: false,
    };
  },
  components: {
    TheMenu,
    TheHeader,
    TheContent,
  },
  methods: {
    handleToggleMenu() {
      this.toggleMenu = !this.toggleMenu;
    },
  },
};
</script>

<style>
@import url("./style/main.css");
</style>
