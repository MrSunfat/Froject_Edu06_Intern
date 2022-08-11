<template>
  <div class="input-icon d-flex">
    <input type="text" class="input-icon__box" />
    <div class="input-icon__icon">
      <img
        :src="urlIcon"
        alt=""
        class="input-icon__icon-img"
      />
    </div>
  </div>
</template>

<script>
import searchIcon from "../../assets/Icons/Ic_seerch.png"

export default {
  name: "InputIcon",
  data() {
    return {
      urlIcon: searchIcon,
    }
  },
  props: {
    placeholderText: {
      type: String,
      default: "Tìm kiếm cán bộ "
    }
  }
};
</script>

<style scoped>
.input-icon {
    height: var(--height-input);
    border: 1px solid var(--border-input);
    border-radius: 4px;
    padding-top: 4px;
    padding-bottom: 4px;
    padding-left: 12px;
}

.input-icon__box {
  border: none;
    border-right: 1px solid var(--border-input);
    height: 100%;
}

.input-icon__box:focus {
    outline: unset;
}

.input-icon__icon {
    padding: 0 8px;
    cursor: pointer;
}

.input-icon__icon-img {
    /* height: 100%; */
}
</style>