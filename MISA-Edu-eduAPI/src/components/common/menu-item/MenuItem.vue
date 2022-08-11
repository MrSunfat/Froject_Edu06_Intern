<template>
  <router-link :to="path" class="menu__item d-flex">
    <div class="menu__item__main d-flex">
      <img class="menu__item__icon" :src="iconItem" :alt="title" />
      <h1 class="subtitle-two menu__item__title">{{ title }}</h1>
    </div>
    <ul class="sub-menu">
      <h5 class="sub-menu__title">
        {{ title }}
      </h5>
      <li
        class="sub-menu__item subtitle-two"
        v-for="subMenu in listSubMenu"
        :key="subMenu.name"
      >
        {{ subMenu.name }}
      </li>
    </ul>
  </router-link>
</template>

<script>
export default {
  name: "MenuItem",
  data() {
    return {
      listSubMenu: [
        {
          name: "Phân phối chương trình",
        },
        {
          name: "Chương trình học",
        },
        {
          name: "Phân công giảng dạy",
        },
        {
          name: "Thời khóa biểu",
        },
      ],
    };
  },
  props: {
    title: String,
    path: String,
    iconItem: String,
  },
};
</script>

<style scoped>
.menu__item {
  display: flex;
  align-items: center;
  padding-left: 5px;
  padding-top: 6px;
  padding-bottom: 6px;
  cursor: pointer;
  height: 100%;
  position: relative;
  text-decoration: none;
}

.menu__item:hover {
  background-color: var(--hover-sidebar-bg);
}

.menu__item .sub-menu {
  margin: 0;
}

.menu__item:hover .sub-menu {
  /* display: block; */
  display: none;
}

.menu__item.active {
  background-color: var(--active-sidebar-bg);
  border-left: 3px solid var(--active-sidebar-color);
  padding-left: 2px;
}

.menu__item.active:hover .sub-menu {
  left: 217px;
  right: -248px;
}

.menu__item__icon {
  width: 38px;
  height: 32px;
}

.menu__item.active .menu__item__title {
  color: var(--active-sidebar-color);
  font-weight: 700;
}

.menu__item__title {
  color: var(--title-color-on-menu-item);
  margin-left: 8px;
  font-weight: 500;
}

#app.menu-no-expand .menu__item__title {
  display: none;
}

/* sub menu */
.sub-menu {
  position: absolute;
  left: var(--menu-width);
  top: 0;
  width: 250px;
  background-color: var(--bg-sub-menu);
  color: #fff;
  padding-left: 0;
  padding-bottom: 15px;
  display: none;
  z-index: 2;
}

.sub-menu.hide {
  display: none;
}

.sub-menu > .sub-menu__item {
  height: 36px;
  padding: 12px 24px;
  list-style-type: none;
}

.sub-menu__title {
  padding-left: 24px;
  margin-top: 24px;
  margin-bottom: 9px;
  cursor: default;
}

.sub-menu__item:hover {
  color: var(--active-sidebar-color);
}

/* no expand */
#app.menu-no-expand .menu .sub-menu {
  left: var(--menu-width-no-expand);
}
</style>