<template>
  <div class="combo-box i-d-flex">
    <button class="reset-btn btn-main">{{ nameBtn }}</button>
    <button class="reset-btn selector">
      <div class="down-icon"></div>
    </button>
  </div>
</template>

<script>
export default {
  name: "ComboBox",
  props: {
    nameBtn: String,
  },
};
</script>

<style scoped>
.combo-box > .btn-main {
  padding: 0 12px;
  background-color: var(--main-bg-btn);
  color: #fff;
  border: none;
  border-top-right-radius: 0;
  border-bottom-right-radius: 0;
}

.combo-box > .btn-main:hover {
  background-color: var(--main-bg-hover-btn);
}

.combo-box > .btn-main.disable {
  background-color: var(--bg-btn-stye-disable);
  color: var(--text-disable-color);
  cursor: not-allowed;
}

.combo-box > .selector {
  padding: 0 12px;
  background-color: var(--selector-bg);
  color: #fff;
  border: none;
  border-top-left-radius: 0;
  border-bottom-left-radius: 0;
  min-width: 0;
}

.combo-box > .selector:hover {
  background-color: var(--selector-bg-hover);
}

.combo-box > .selector.disable {
  background-color: var(--selector-bg-disable);
  cursor: not-allowed;
}

.down-icon {
  background: url("../../../assets/Icons/ic_sprites.svg") no-repeat -185px -165px;
  width: 11px;
  height: 7px;
}
</style>