<template>
  <div class="dropdown-one" :class="{ binding: binding }">
    <label :for="label" class="subtitle-two d-flex">
      {{ label }}<span style="color: red">*</span>
    </label>
    <div
      :id="label"
      :tabindex="tabindex"
      class="ip-default dropdown-one__container d-flex"
      :class="{ active: activeDropdown }"
      @click="handleOpenDropdown"
      @keyup.tab="handleTabUp"
      @keydown.tab="handleTabDown"
      @keyup.down="handleKeyDown"
      @keyup.up="handleKeyUp"
      @keyup.enter="handleKeyEnter"
      @keyup.esc="handleEscKey"
    >
      <h1 class="subtitle-two content">{{ dbDropdown.optionSelected }}</h1>
      <div class="dropdown-one__down-icon d-flex">
        <img :src="downIconUrl" alt="" class="down-icon" />
      </div>
    </div>
    <ul
      class="dropdown-one__options"
      :class="this.openDropdown && 'show'"
      ref="scrollOptions"
    >
      <li
        class="dropdown-one__option d-flex"
        :class="{
          selected: option.selected,
          hover:
            this.dbDropdown.listSubjectGroup[this.pointerOption]?.name ===
            option.name,
        }"
        v-for="option in dbDropdown.listSubjectGroup"
        :key="option.name"
        @click="chooseOption(option.name)"
      >
        {{ option.name }}
      </li>
    </ul>
  </div>
</template>

<script>
// icon
import downIcon from "../../assets/Icons/ic_Chevron.png";

export default {
  name: "DropDownOne",
  props: {
    label: String,
    binding: {
      type: Boolean,
      default: false,
    },
    dbDropdown: Object,
    tabindex: {
      type: Number,
      default: 1,
    },
  },
  data() {
    return {
      openDropdown: false,
      activeDropdown: false,
      downIconUrl: downIcon,
      pointerOption: 0,
    };
  },
  methods: {
    /*
      Mở các option
      Author: Tran Danh (16/7/2022)
    */
    handleOpenDropdown() {
      this.openDropdown = !this.openDropdown;
    },
    /**
     * Đóng các option
     * Author: Tran Danh (16/7/2022)
     */
    handleCloseOption() {
      this.openDropdown = false;
    },
    /**
     * Truyền tên option và kích hoạt sự kiện -> chọn option
     * Author: Tran Danh (16/7/2022)
     */
    chooseOption(nameOption) {
      this.$emit("changeOption", nameOption);
      this.handleCloseOption();
    },
    /**
     * Khi nhấn nút down thì sẽ lựa chọn option trong dropdown option
     * Tran Danh (27/7/2022)
     */
    handleKeyDown() {
      this.openDropdown = true;
      if (this.pointerOption + 1 < this.dbDropdown.listSubjectGroup.length) {
        this.pointerOption++;
      }
      this.$refs.scrollOptions.scrollTop = 20 * this.pointerOption;
    },
    /**
     *  Khi nhấn nút up thì sẽ lựa chọn option lên trên trong dropdown option
     *  Tran Danh (27/7/2022)
     */
    handleKeyUp() {
      if (this.pointerOption - 1 >= 0) {
        this.pointerOption--;
      }
      this.$refs.scrollOptions.scrollTop = 20 * this.pointerOption;
    },
    /**
     * Mở Dropdown options
     * Tran Danh (27/7/2022)
     */
    handleKeyEnter() {
      this.openDropdown = !this.openDropdown;
      this.$emit(
        "changeOption",
        this.dbDropdown.listSubjectGroup[this.pointerOption]?.name
      );
      console.log("Dropdown");
    },
    /**
     * Ẩn Dropdown options bằng phím Esc
     * Tran Danh (27/7/2022)
     */
    handleEscKey() {
      this.openDropdown = false;
    },
    /**
     * Hiện active của dropdown chính khi tab đến
     * Tran Danh (27/7/2022)
     */
    handleTabUp() {
      this.activeDropdown = !this.activeDropdown;
    },
    /**
     * Ẩn active của dropdown chính khi tab đi
     * Tran Danh (27/7/2022)
     */
    handleTabDown() {
      this.activeDropdown = !this.activeDropdown;
      this.openDropdown = false;
    },
  },
  computed: {},
};
</script>

<style>
.dropdown-one {
  display: grid;
  grid-template-columns: var(--width-title-h-input) calc(
      100% - var(--width-title-h-input)
    );
  position: relative;
  margin-bottom: 12px;
}

.dropdown-one > label {
  color: var(--label-input-color);
}

.dropdown-one label.subtitle-two > span {
  display: none;
}

.dropdown-one.binding label.subtitle-two > span {
  display: block;
}

.dropdown-one__container {
  justify-content: space-between;
  padding-left: 12px;
  margin-left: 12px;
  cursor: pointer;
}

.dropdown-one__container.active {
  border: 1px solid var(--active-border-input);
}

.dropdown-one__container > .content {
  padding: 0;
  color: var(--main-color-in-form);
}

.dropdown-one__down-icon {
  margin: 0 5px;
  height: 100%;
}

/* option */

.dropdown-one__options {
  position: absolute;
  content: "";
  top: calc(var(--height-input) + 8px);
  /* left: 0; */
  right: 0;
  margin: 0;
  padding-left: 0;
  padding-top: 4px;
  padding-bottom: 4px;
  background-color: #fff;
  border-radius: 4px;
  box-shadow: 0 0 5px #b1b1b1;
  overflow: hidden;
  z-index: 2;
  width: calc(100% - var(--width-title-h-input) - 12px);
  max-height: 125px;
  overflow-y: scroll;
  display: none;
}

.dropdown-one__options::-webkit-scrollbar {
  width: 5px;
}

.dropdown-one__options::-webkit-scrollbar-track {
  box-shadow: inset 0 0 5px #e5e5e5;
  border-radius: 4px;
}

.dropdown-one__options::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
}

.dropdown-one__options.show {
  display: block;
}

.dropdown-one__option {
  list-style-type: none;
  height: var(--height-option-dropdown);
  padding-left: 16px;
  color: var(--main-color-in-form);
}

.dropdown-one__option.hover {
  background: var(--hover-color);
}

.dropdown-one__option:hover {
  background: var(--hover-color);
  cursor: pointer;
}

.dropdown-one__option.selected {
  background: var(--row-focus-color-on-grid-color);
}
</style>