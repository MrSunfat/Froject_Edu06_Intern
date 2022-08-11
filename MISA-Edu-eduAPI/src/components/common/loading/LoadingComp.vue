<template>
  <div class="overlay d-flex">
    <div class="loading d-flex">
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <h4 class="title loading__title">{{ title }}</h4>
    </div>
  </div>
</template>

<script>
import constants from "../../../scripts/constants/constants";

export default {
  name: "LoadingComp",
  data() {
    return {
      title: constants.titleLoading,
    };
  },
};
</script>

<style scoped>
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.25);
  justify-content: center;
  z-index: 1;
}

.loading {
  /* display: inline-block; */
  justify-content: center;
  position: relative;
  width: 120px;
  height: 64px;
  background: #fff;
  border-radius: 4px;
}

.loading__title {
  position: absolute;
  top: 35px;
  left: 50%;
  transform: translateX(-50%);
  min-width: max-content;
}

.loading div {
  position: absolute;
  top: 18px;
  width: 8px;
  height: 8px;
  border-radius: 50%;
  background: #fff;
  animation-timing-function: cubic-bezier(0, 1, 1, 0);
}
.loading div:nth-child(1) {
  left: 36px;
  background-color: #e15b64;
  animation: loading1 0.6s infinite;
}
.loading div:nth-child(2) {
  left: 36px;
  background-color: #f47e60;
  animation: loading2 0.6s infinite;
}
.loading div:nth-child(3) {
  left: 58px;
  background-color: #f8b26a;
  animation: loading2 0.6s infinite;
}
.loading div:nth-child(4) {
  left: 75px;
  background-color: #b3c38c;
  animation: loading3 0.6s infinite;
}
@keyframes loading1 {
  0% {
    transform: scale(0);
  }
  100% {
    transform: scale(1);
  }
}
@keyframes loading3 {
  0% {
    transform: scale(1);
  }
  100% {
    transform: scale(0);
  }
}
@keyframes loading2 {
  0% {
    transform: translate(0, 0);
  }
  100% {
    transform: translate(24px, 0);
  }
}
</style>