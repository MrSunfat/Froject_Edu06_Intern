<template>
  <div class="tag-comp i-d-flex">
    <label class="label subtitle-two">{{ label }}</label>
    <div
      class="container ip-default i-d-flex"
      :tabindex="tabindex"
      :class="{ active: this.activeContainer }"
      @keyup.tab="handleTabUp"
      @keydown.tab="handleTabDown"
      @keyup.down="handleDown"
      @keydown.up="handleUp"
      @keyup.enter="handleEnterKey"
      @keyup.esc="handleEscKey"
    >
      <div class="tag-comp__main d-flex">
        <div class="tags-wrapper d-flex">
          <div
            class="tag__option d-flex"
            v-for="subject in listSubject"
            :key="subject.id"
            v-show="handleShowListTag(subject.selected)"
          >
            <h1 class="content subtitle-two d-flex">{{ subject.name }}</h1>
            <h1 class="content subtitle-two d-flex" v-show="checkAll">
              {{ allOption }}
            </h1>
            <div class="delete__option c-p">
              <img
                :src="deleteIconUrl"
                alt=""
                @click="deleteSubject(subject.id)"
              />
            </div>
          </div>
          <div class="tag__option d-flex" v-show="checkAll">
            <h1 class="content subtitle-two d-flex">
              {{ allOption }}
            </h1>
            <div class="delete__option c-p">
              <img :src="deleteIconUrl" alt="" @click="deleteSubject(false)" />
            </div>
          </div>
        </div>
        <input
          type="text"
          class="main__input"
          @focus="handleShowListSubject"
          v-model="inputValue"
        />
      </div>
      <div class="tag-comp__second d-flex c-p" @click="handleShowListSubject">
        <img :src="downIconUrl" alt="" class="down-icon" />
      </div>
      <ul
        class="list-student"
        :class="{ show: this.showListSubject }"
        ref="scrollOptions"
      >
        <li class="list-title d-flex" @click="handleCheckAll">
          <button
            class="checkbox-input d-flex mg-r-8"
            :class="{ active: this.checkAll }"
            @click="handleCheckAll"
          >
            <img
              :src="checkboxInactive"
              alt="checkbox-icon"
              class="checkbox-input__icon"
            />
            <img
              :src="checkboxHover"
              alt="checkbox-icon"
              class="checkbox-input__icon-hover"
            />
            <img
              :src="checkboxActive"
              alt="checkbox-icon"
              class="checkbox-input__icon-checked"
            />
          </button>
          <h1 class="subtitle-one">T???t c???</h1>
        </li>
        <li
          class="student-item d-flex c-p"
          :class="{ hover: subject.id === this.listSubject[pointerOption].id }"
          v-for="subject in handleFilterListSubj"
          :key="subject.id"
          @click="selectSubject(subject.id)"
        >
          <button
            class="checkbox-input d-flex mg-r-8"
            :class="{ active: subject.selected }"
            @click="selectSubject(subject.id)"
          >
            <img
              :src="checkboxInactive"
              alt="checkbox-icon"
              class="checkbox-input__icon"
            />
            <img
              :src="checkboxHover"
              alt="checkbox-icon"
              class="checkbox-input__icon-hover"
            />
            <img
              :src="checkboxActive"
              alt="checkbox-icon"
              class="checkbox-input__icon-checked"
            />
          </button>
          <h1 class="subtitle-one">{{ subject.name }}</h1>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
// icon
import downIcon from "@/assets/Icons/ic_Chevron.png";
import deleteIcon from "@/assets/Icons/ic_cancel.png";
import checkboxInactive from "@/assets/Icons/ic_Checkbox_Inactive.png";
import checkboxHover from "@/assets/Icons/ic_Checkbox_Hover.png";
import checkboxActive from "@/assets/Icons/ic_Checkbox_Active.png";

export default {
  name: "TagComp",
  props: {
    label: {
      type: String,
      default: "QL theo m??n",
    },
    listSubject: {
      type: Array,
      default: () => [
        {
          id: "Math",
          name: "To??n",
          selected: true,
        },
        {
          id: "Physics",
          name: "V???t l??",
          selected: false,
        },
        {
          id: "Chemistry",
          name: "H??a h???c",
          selected: true,
        },
        {
          id: "Bio",
          name: "Sinh h???c",
          selected: true,
        },
        {
          id: "History",
          name: "L???ch s???",
          selected: true,
        },
      ],
    },
    typeTag: {
      type: String,
      default: "subject",
    },
    tabindex: {
      type: Number,
      dafault: 0,
    },
  },
  data() {
    return {
      checkboxInactive,
      checkboxHover,
      checkboxActive,
      inputValue: "",
      allOption: "T???t c???",
      downIconUrl: downIcon,
      deleteIconUrl: deleteIcon,
      showListSubject: false,
      checkAll: false,
      activeContainer: false,
      showListOption: false,
      pointerOption: 0,
    };
  },
  methods: {
    /**
     * Show | un show c??c m??n h???c
     * Author: Tran Danh (18/7/2022)
     */
    handleShowListSubject() {
      this.showListSubject = !this.showListSubject;
    },
    /**
     * K??ch ho???t s??? ki???n -> check | uncheck m??n h???c
     * Author: Tran Danh (18/7/2022)
     */
    selectSubject(idSubject) {
      const data = {
        id: idSubject,
        name: this.typeTag,
      };
      this.$emit("selectSubject", data);
      this.showCheckAll();
    },
    /**
     *  X??a s??? l???a ch???n v??? m??n h???c ????
     *  Author: Tran Danh (18/7/2022)
     */
    deleteSubject(idSubject) {
      if (!idSubject) {
        this.checkAll = !this.checkAll;
      }
      const data = {
        value: idSubject,
        name: this.typeTag,
      };
      this.$emit("deleteSubject", data);
    },
    /**
     * X??? l?? khi checkAll
     * Author: Tran Danh (18/7/2022)
     */
    handleCheckAll() {
      const data = {
        value: this.checkAll,
        name: this.typeTag,
      };
      this.$emit("isCheckAll", data);
      this.checkAll = !this.checkAll;
    },
    /**
     * Hi???n checkAll khi check h???t c??c subject v?? ng?????c l???i
     * Author: Tran Danh (18/7/2022)
     */
    showCheckAll() {
      let numberSelected = 0;
      for (let i = 0; i < this.listSubject.length; i++) {
        if (this.listSubject[i].selected) {
          numberSelected += 1;
        }
      }

      this.checkAll = numberSelected === this.listSubject.length;
    },
    /**
     * X??? l?? khi nh???n check all -> g???i 1 s??? ki???n l??n
     * AUthor: Tran Danh (18/7/2022)
     */
    isCheckAll() {},
    /**
     * Hi???n c??c tag
     * Author: Tran Danh (19/7/2022)
     */
    handleShowListTag(selectedSubject) {
      if (!this.checkAll) {
        return selectedSubject;
      }
      return false;
    },
    // X??? l?? s??? ki???n t???t = ph??m cho tag
    /**
     * X??? l?? khi tab ?????n -> active l??n
     * Tran Danh (27/7/2022)
     */
    handleTabUp() {
      this.activeContainer = true;
    },
    /**
     * ???n list option = esc key
     * Tran Danh (28/7/2022)
     */
    handleEscKey() {
      this.showListSubject = false;
    },
    /**
     * X??? l?? khi tab ??i -> t???t active ??i v?? ????ng list option
     * Tran Danh (27/7/2022)
     */
    handleTabDown() {
      this.activeContainer = false;
      this.showListSubject = false;
    },
    /**
     * Di chuy???n xu???ng trong list option
     * TRan Danh (27/7/2022)
     */
    handleDown() {
      if (this.pointerOption + 1 < this.listSubject.length) {
        this.pointerOption++;
      }
      this.$refs.scrollOptions.scrollTop = 20 * this.pointerOption;
    },
    /**
     * Di chuy???n l??n trong list option
     * TRan Danh (27/7/2022)
     */
    handleUp() {
      if (this.pointerOption - 1 >= 0) {
        this.pointerOption--;
      }
      this.$refs.scrollOptions.scrollTop = 20 * this.pointerOption;
    },
    /**
     * X??? l?? khi nh???n enter
     * Tran Danh (27/7/2022)
     */
    handleEnterKey() {
      this.showListSubject = true;
      this.selectSubject(this.listSubject[this.pointerOption].id);
    },
  },
  computed: {
    handleFilterListSubj() {
      if (!this.inputValue) return this.listSubject;
      return this.listSubject.filter((subject) =>
        subject.name.includes(this.inputValue)
      );
    },
  },
};
</script>

<style scoped>
.mg-r-8 {
  margin-right: 8px;
}

.tag-comp {
  position: relative;
  display: grid;
  grid-template-columns: var(--width-title-h-input) calc(
      100% - var(--width-title-h-input)
    );
  margin-bottom: 18px;
  min-width: 100%;
  height: var(--height-input);
}

.tag-comp > label {
  color: var(--label-input-color);
}

.tag-comp .container {
  position: relative;
  margin-left: 12px;
  padding-top: 2px;
  padding-bottom: 2px;
  justify-content: space-between;
}

.tag-comp .container.active {
  border: 1px solid var(--active-border-input);
}

.tag-comp__main {
  height: 100%;
  overflow-x: hidden;
  overflow-y: hidden;
}

.tag-comp__main:hover {
  overflow-x: overlay;
}

.tag-comp__main::-webkit-scrollbar {
  width: 10px;
  cursor: pointer;
}
.tag-comp__main::-webkit-scrollbar:horizontal {
  height: 4px;
}
.tag-comp__main::-webkit-scrollbar-track {
  background-color: transparent;
}
.tag-comp__main::-webkit-scrollbar-thumb {
  border-radius: 10px;
  background: #fff;
  box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.5);
}

.tag-comp .main__input {
  border: none;
  padding-left: 12px;
  /* width: 100%; */
  height: 100%;
  background: transparent;
}

.tag-comp .main__input:focus {
  outline: unset;
}

.tag-comp__second {
  padding: 0 5px;
  height: 100%;
}

.tag-comp .list-student {
  position: absolute;
  top: calc(var(--height-input) + 8px);
  left: 0;
  right: 0;
  padding: 4px 0;
  margin-top: 0;
  margin-bottom: -1px;
  background: #fff;
  box-shadow: 0 0 5px #b1b1b1;
  justify-content: space-around;
  max-height: 180px;
  border-radius: 4px;
  overflow-y: scroll;
  display: none;
}

.tag-comp .list-student.show {
  display: block;
  z-index: 2;
}

.tag-comp .list-student::-webkit-scrollbar {
  width: 5px;
}

.tag-comp .list-student::-webkit-scrollbar-track {
  box-shadow: inset 0 0 5px #e5e5e5;
  border-radius: 4px;
}

.tag-comp .list-student::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
}

.tag-comp .list-title {
  height: 45px;
  padding: 0 12px;
  border-bottom: 1px solid #d5d5d5;
}

.tag-comp .student-item {
  padding-top: 8px;
  padding-left: 12px;
  padding-bottom: 8px;
}

.tag-comp .student-item.hover {
  background-color: var(--hover-color);
}

.tag-comp .student-item:hover {
  background-color: var(--hover-color);
}

.tag__option {
  justify-content: space-between;
  background: #ccc;
  border-radius: 4px;
  height: 100%;
  margin: 2px 5px;
  padding-left: 10px;
  padding-right: 0;
  min-width: max-content;
}

.tag__option .content {
  color: #000;
}

.delete__option {
  height: 100%;
}

.delete__option:hover {
  color: #ff6363;
}

/* checkbox */
.checkbox-input {
  border: none;
  cursor: pointer;
  background-color: transparent;
}

.checkbox-input__icon-hover,
.checkbox-input__icon-checked {
  display: none;
}

.checkbox-input:hover .checkbox-input__icon {
  display: none;
}

.checkbox-input:hover .checkbox-input__icon-hover {
  display: block;
}

.checkbox-input.active .checkbox-input__icon {
  display: none;
}

.checkbox-input.active .checkbox-input__icon-hover {
  display: none;
}

.checkbox-input.active .checkbox-input__icon {
  display: none;
}

.checkbox-input.active .checkbox-input__icon-checked {
  display: block;
}
</style>