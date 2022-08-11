<template>
  <button
    class="reset-btn base-btn i-d-flex"
    :class="type"
    @click="handleClick"
  >
    <div class="base-btn__icon d-flex" v-show="validateUrlIcon">
      <img :src="urlIcon" alt="btn icon" class="base-btn__icon-img" />
    </div>
    <div class="base-btn__text" v-show="validateNameBtn">
      {{ nameBtn }}
    </div>
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: "BaseBtn",
  data() {
    return {};
  },
  props: {
    nameBtn: String,
    handleClick: {
      type: Function,
      default: () => {},
    },
    type: String,
    urlIcon: String,
  },
  methods: {},
  computed: {
    validateUrlIcon() {
      if (this.urlIcon === undefined || this.urlIcon.length === 0) {
        return false;
      }
      return true;
    },
    validateNameBtn() {
      if (this.nameBtn === undefined || this.nameBtn.length === 0) {
        return false;
      }
      return true;
    },
  },
};
</script>

<style scoped>
/* {
      'primary-btn': false,
      'second-btn': false,
      'icon-and-text-btn': true,
      'icon-btn': true,
    } */

/* base btn */
.base-btn {
  padding: 0 12px;
}

.base-btn__icon {
  margin-right: 12px;
}
.base-btn__text {
  font-family: Open Sans SemiBold;
  font-size: 13px;
}

/* primary btn */
.primary-btn {
  background-color: var(--main-bg-btn);
  color: #fff;
  border: none;
}

.primary-btn:hover {
  background-color: var(--main-bg-hover-btn);
}

.primary-btn.disable {
  background-color: var(--bg-btn-stye-disable);
  color: var(--text-disable-color);
  cursor: not-allowed;
}

/* second btn */
.second-btn {
  border: 1px solid var(--second-btn-bd);
  color: var(--style-two-normal-text-color);
  background-color: var(--second-bg-btn);
}

.second-btn:hover {
  background-color: var(--second-bg-hover-btn);
}

.second-btn.disable {
  background-color: var(--second-bg-btn);
  color: var(--text-disable-color);
  cursor: not-allowed;
}

/* icon & text btn */
.icon-and-text-btn {
  border: none;
  border: 1px solid var(--style-four-btn-bd);
  color: var(--style-four-text-color-btn);
  background-color: #fff;
}

.icon-and-text-btn:hover {
  background-color: var(--main-bg-btn);
  color: #fff;
  border: none;
}

.icon-and-text-btn.disable {
  background-color: var(--bg-btn-stye-disable);
  color: var(--text-disable-color);
  border: none;
  cursor: not-allowed;
}

/* icon btn */
.icon-btn {
  border: 1px solid var(--second-btn-bd);
  background: #fff;
  min-width: 0 !important;
  padding: 0 3px;
}

.icon-btn > .base-btn__icon {
  margin-right: 0;
}

.icon-btn:hover {
  background-color: var(--style-five-bg-hover);
}

.icon-btn.disable {
  cursor: not-allowed;
}
</style>