<template>
  <div class="content">
    <!-- content -->
    <!-- <system-page /> -->
    <router-view />
  </div>
</template>

<script>
// import SystemPage from "../pages/SystemPage.vue";
export default {
  name: "TheContent",
  data() {
    return {
      closeFormAdd: false,
    };
  },
  components: {
    // SystemPage,
  },
  methods: {},
};
</script>

<style scoped>
.content {
  position: relative;
  overflow: hidden;
  padding: 0 16px;
}
</style>