<template>
  <div class="popup-notify-overlay">
    <div class="popup-notify">
      <h1 class="title popup-notify__title">{{ titleNotify }}</h1>
      <p class="subtitle-one">
        {{ !showCancelBtn ? formatContent : this.contentNotify }}
      </p>
      <div class="popup-notify__btns mg-t-16">
        <base-btn
          class="close-btn"
          nameBtn="Đóng"
          :type="typeBtn.SECONDARY"
          :handleClick="handleClosePopupNotify"
        />
        <base-btn
          v-show="showCancelBtn"
          class="cancel-btn mg-l-8"
          nameBtn="Không"
          :type="typeBtn.SECONDARY"
          :handleClick="handleClosePopupAndForm"
        />
        <base-btn
          class="access-btn mg-l-8"
          nameBtn="Xác nhận"
          :type="typeBtn.PRIMARY"
          :handleClick="!showCancelBtn ? handleConfirm : handleConfirmValidateForm"
        />
      </div>
      <div class="popup-notify__close c-p" @click="handleClosePopupNotify">
        <img src="../../assets/Icons/ic_X_2.png" alt="" class="close-icon" />
      </div>
      <loading-comp v-if="this.isShowLoading" />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import typeBtn from "../../scripts/enum/typeBtn";
import BaseBtn from "../common/button/BaseBtn.vue";
export default {
  name: "PopupNotify",
  data() {
    return {
      typeBtn: typeBtn,
    };
  },
  props: {
    titleNotify: {
      type: String,
      default: "Thông báo",
    },
    contentNotify: {
      type: String,
      default: "Bạn có chắc chắn xóa Cán bộ, giáo viên đang chọn không ?",
    },
    showCancelBtn: {
      type: Boolean,
      default: false,
    },
  },
  components: { BaseBtn },
  methods: {
    /**
     * Đóng popup
     * Author: Tran Danh (21/7/2022)
     */
    handleClosePopupNotify() {
      // console.log(message);
      this.$emit("closePopupNotify");
      this.SET_TEACHER_CURRENT({ id: "", name: "" });
    },
    /**
     * Xác nhận xóa giáo viên qua id
     * Author: Tran Danh (21/7/2022)
     */
    handleConfirm() {
      // 1. Ưu tiên xóa từng hàng giáo viên trước
      if (this.teacherIdCurrent) {
        this.deleteTeacher(this.teacherIdCurrent);
      }
      // 2. Mới đến, xóa những giáo viên đã chọn
      else if (this.listTeacherIdDelete.length > 0) {
        this.deleteListTeacher(this.listTeacherIdDelete);
        this.setListTeacherIdDelete([]);
      }
      this.SHOW_LOADING();
      this.$emit("showToastSuccess");
      this.handleClosePopupNotify();
    },
    ...mapActions(["deleteTeacher", "deleteListTeacher"]),
    ...mapMutations([
      "SHOW_LOADING",
      "HIDE_LOADING",
      "SET_TEACHER_CURRENT",
      "setListTeacherIdDelete",
      "setEmptyTeacher",
    ]),
    /**
     * Xác nhận validate form
     * Author: Tran Danh (21/7/2022)
     */
    handleConfirmValidateForm() {
      this.$emit("validateForm");
      this.handleClosePopupNotify();
    },
    /**
     * Đóng popup và form
     * Tran Danh (29/7/2022)
     */
    handleClosePopupAndForm() {
      this.$emit("closePopupAndForm");
      this.SET_TEACHER_CURRENT({ id: "", name: "" });
      this.setEmptyTeacher();
    },
  },
  computed: {
    ...mapGetters([
      "teacherIdCurrent",
      "teacherNameCurrent",
      "isShowLoading",
      "teachers",
      "listTeacherIdDelete",
    ]),
    /**
     * format content popup
     * Author: Tran Danh (25/7/2022)
     */
    formatContent() {
      return `Bạn có chắc chắn xóa Cán bộ, giáo viên ${this.teacherNameCurrent} đang chọn không ?`;
    },
  },
  mounted() {
    setTimeout(
      () => {
        this.isShowToast = false;
        // console.log(this.linkToast);
      },
      this.toastInfo?.title ? 5000 : 3000
    );
  },
  watch: {
    teachers() {
      this.HIDE_LOADING();
    },
  },
};
</script>

<style scoped>
.popup-notify-overlay {
  position: fixed;
  background-color: rgba(0, 0, 0, 0.85);
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 1;
  /* display: none; */
}

/* .popup-notify-overlay.show {
  display: flex;
  align-items: center;
} */

.popup-notify {
  position: relative;
  top: 50%;
  transform: translateY(-50%);
  width: 400px;
  background-color: #fff;
  margin: 0 auto;
  padding: 20px 24px;
  border-radius: 4px;
}

.popup-notify__title {
  font-size: 22px !important;
  margin-bottom: 12px;
}

.popup-notify__btns {
  text-align: right;
}

.popup-notify__close {
  position: absolute;
  top: 5px;
  right: 5px;
}
</style>