<template>
  <div class="base-toast toast-messager d-flex" :class="typeToast">
    <div class="base-toast__icon">
      <img :src="handleTypeIconToast" alt="" class="base-toast__icon-img" />
    </div>
    <div class="base-toast__main">
      <h1 class="title base-toast__title mg-bt-6">
        {{ handleValueTitleToast }}
      </h1>
      <p class="subtitle-two base-toast__subtitle">
        {{ handleValueConentToast }}
        <a
          :href="this.linkToast.urlLink"
          class="link-text c-p"
          v-show="isHaveLinkToast"
          >{{ this.linkToast.title }}</a
        >
      </p>
    </div>
    <div
      class="toast-messager__close d-flex c-p"
      v-show="isHaveLinkToast"
      @click="handleCloseToast"
    >
      <img :src="deleteIconUrl" alt="" class="toast-messager__close-img" />
    </div>
  </div>
</template>

<script>
import typeToast from "../../../scripts/enum/typeToast";
// icon toast
import successIcon from "../../../assets/Icons/ic_ToastMessage_Success.png";
import failIcon from "../../../assets/Icons/ic_ToastMessage_Fail.png";
import warnIcon from "../../../assets/Icons/ic_ToastMessage_Warning.png";
import infoIcon from "../../../assets/Icons/ic_ToastMessage_Info.png";
import deleteIcon from "../../../assets/Icons/ic_X_40.png";

export default {
  name: "BaseToast",
  props: {
    titleToast: {
      type: String,
      default: "Thông báo",
    },
    contentToast: {
      type: String,
      default: "Đã thành công",
    },
    linkToast: {
      type: Object,
      default: () => ({
        title: "",
        urlLink: "",
      }),
    },
    typeToast: {
      type: String,
      default: typeToast.SUCCESS,
    },
  },
  data() {
    return {
      deleteIconUrl: deleteIcon,
      title: "Thông báo",
    };
  },
  methods: {
    /**
     * Đóng toast message
     * Author: Tran Danh (17/7/2022)
     */
    handleCloseToast() {
      this.$emit("closeToast");
    },
  },
  computed: {
    /**
     * Xử lý các thể loại toast message -> icon toast
     * Author: Tran Danh (17/7/2022)
     */
    handleTypeIconToast() {
      switch (this.typeToast) {
        case typeToast.SUCCESS:
          return successIcon;
        case typeToast.FAIL:
          return failIcon;
        case typeToast.WARNING:
          return warnIcon;
        case typeToast.INFO:
          return infoIcon;
        default:
          return successIcon;
      }
    },
    /**
     * Xét việc show link toast và show delete icon
     * Author: Tran Danh (17/7/2022)
     */
    isHaveLinkToast() {
      return this.linkToast.title ? true : false;
    },
    /**
     * Xử lý value của conentToast
     * Author: Tran Danh (20/7/2022)
     */
    handleValueConentToast() {
      if (!this.contentToast || this.contentToast.length === 0)
        return "Đã thành công";
      return this.contentToast;
    },
    /**
     * Xử lý value của conentToast
     * Author: Tran Danh (20/7/2022)
     */
    handleValueTitleToast() {
      if (!this.titleToast || this.titleToast.length === 0) return "Thông báo";
      return this.titleToast;
    },
  },
  mounted() {
    setTimeout(
      () => {
        this.handleCloseToast();
      },
      this.linkToast.title ? 5000 : 3000
    );
  },
};
</script>

<style scoped>
.base-toast {
  position: fixed;
  top: calc(50vh + 200px);
  right: 5px;
  border-left: 5px solid var(--main-color);
  transition: all 0.35s ease-in;
}

.base-toast__icon {
  margin-right: 18px;
}

.base-toast__subtitle {
  margin: 0;
}

/* success */
.toast-success {
  border-left: 5px solid var(--main-color);
}

/* fail */
.toast-fail {
  border-left: 5px solid var(--hover-delete-icon-color);
}

/* warning */
.toast-warning {
  border-left: 5px solid var(--warn-color);
}

/* info */
.toast-info {
  border-left: 5px solid var(--link-color);
}

/* toast messager default */
.toast-messager {
  min-height: 80px;
  min-width: 300px;
  background-color: #fff;
  box-shadow: 0 0 5px #b1b1b1;
  border-radius: 4px;
  padding: 8px 18px 7px 15px;
  /* right: -350px; */
}

.toast-messager.show {
  right: 16px;
}

.toast-messager__close {
  margin-left: 30px;
  margin-right: -10px;
}

.link-text {
  text-decoration: none;
  color: var(--link-color);
}
</style>