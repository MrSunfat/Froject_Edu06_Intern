<template>
  <div class="system-page">
    <div class="system-page__control d-flex">
      <div class="left">
        <div class="input-icon d-flex">
          <input
            type="text"
            class="input-icon__box"
            placeholder="Tìm kiếm cán bộ, giáo viên"
            v-model="search.content"
            @change="handleFilter"
          />
          <div class="input-icon__icon" @click="handleFilter" title="Tìm kiếm">
            <img :src="urlSeachIcon" alt="" class="input-icon__icon-img" />
          </div>
        </div>
      </div>
      <div class="right d-flex">
        <base-btn
          nameBtn="Thêm"
          :type="typeBtnEnum.PRIMARY"
          :handleClick="handleOpenFormAddTeacher"
        >
        </base-btn>
        <base-btn
          class="export-icon"
          nameBtn="Xuất khẩu"
          :handleClick="handleExportExcelTeacher"
          :type="typeBtnEnum.SECONDARY"
        />
        <base-btn
          :urlIcon="urlMoreIcon"
          :type="typeBtnEnum.ICON"
          :handleClick="handleToggleDeleteRows"
          :class="{ 'show-delete-rows': this.isShowDeleteRows }"
        >
          <div class="delete-rows d-flex" @click="handleDeleteListTeacher">
            <img :src="urlDeleteIcon" alt="delete icon" class="icon" />
            <h1 class="title-icon">Xóa</h1>
          </div>
        </base-btn>
      </div>
    </div>
    <table-teachers
      class="system-page__table"
      @showEditForm="handleShowEditForm"
      @showToastOnRow="handleOpenToastOnRow"
    />
    <footer-bar-pagination
      :searchText="this.search"
      @setIdxWhenSearchText="handleSetIdxWhenChangeSearchText"
    />
    <form-add-teacher
      v-if="!this.closeFormTeacher"
      ref="formAddTeacher"
      @closeFormAddTeacher="handleCloseFormAddTeacher"
      @successEdit="handleSuccessEdit"
      @closeWhenSuccess="handleCloseWhenSuccess"
      :titleForm="this.titleForm"
    />
    <popup-notify
      v-if="!this.closePopupNotify"
      :showCancelBtn="showCancelBtn"
      :titleNotify="this.detailPopup.title"
      :contentNotify="this.detailPopup.content"
      @closePopupNotify="handleClosePopupNotify"
      @closePopupAndForm="handleClosePopupAndForm"
      @showToastSuccess="handleOpenToast"
      @validateForm="handleConfirmValidateForm"
    />
    <base-toast
      v-if="isShowToast"
      v-bind="detailToast"
      @closeToast="handleCloseToast"
    />
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapActions } from "vuex";
// type enum
import typeBtn from "@/scripts/enum/typeBtn";
import typeToastEnum from "@/scripts/enum/typeToast";
import BaseToast from "@/components/common/toast-message/BaseToast.vue";
import BaseBtn from "@/components/common/button/BaseBtn.vue";
import TableTeachers from "@/components/common/table/TableTeachers.vue";
import FooterBarPagination from "@/components/common/footer-bar/FooterBarPagination.vue";
import PopupNotify from "@/components/common/popup/PopupNotify.vue";
import FormAddTeacher from "@/components/common/form/FormAddTeacher.vue";
// icon
import moreIcon from "../../assets/Icons/ic_More.png";
import deleteIcon from "../../assets/Icons/ic_delete.png";
import searchIcon from "../../assets/Icons/Ic_seerch.png";
import axios from "axios";
import urlTeachers from "@/scripts/constants/urlTeachers";
import constants from "@/scripts/constants/constants";
export default {
  name: "SystemPage",
  data() {
    return {
      urlMoreIcon: moreIcon,
      urlDeleteIcon: deleteIcon,
      urlSeachIcon: searchIcon,
      closeFormTeacher: true,
      closePopupNotify: true,
      showCancelBtn: false,
      isShowDeleteRows: false,
      isChangeTeacher: false,
      typeBtnEnum: {
        PRIMARY: typeBtn.PRIMARY,
        SECONDARY: typeBtn.SECONDARY,
        ICONTEXT: typeBtn.ICONTEXT,
        ICON: typeBtn.ICON,
      },
      listTooltipContent: {
        add: "Thêm cán bộ giáo viên",
        delete: "Xóa giáo viên",
        edit: "Sửa giáo viên",
      },
      detailToast: {
        titleToast: "Xóa thất bại",
        contentToast: "Không có giáo viên nào!",
        typeToast: typeToastEnum.FAIL,
      },
      detailPopup: {
        title: "Thông báo",
        content: "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không ?",
      },
      isShowToast: false,
      titleForm: "Thêm",
      search: {
        pageNumber: 1,
        content: "",
      },
    };
  },
  components: {
    BaseBtn,
    TableTeachers,
    FooterBarPagination,
    PopupNotify,
    FormAddTeacher,
    BaseToast,
  },
  methods: {
    ...mapMutations([
      "setIdxPage",
      "setEmptyTeacher",
      "setTeacherCurrent",
      "setEmptyPrevTeacher",
    ]),
    /**
     * Đóng popup thông báo
     * Author: Tran Danh (16/7/2022)
     */
    handleClosePopupNotify() {
      this.closePopupNotify = true;
      this.setDetailToast(
        "Thành công",
        "Xóa giáo viên thành công",
        typeToastEnum.SUCCESS
      );
    },
    /**
     * Mở popup thông báo
     * Author: Tran Danh (16/7/2022)
     */
    handleOpenPopupNotify() {
      this.closePopupNotify = false;
    },
    /**
     * Đóng form thêm giáo viên và kiểm tra sự thay đổi
     * Author: Tran Danh (16/7/2022)
     */
    handleCloseFormAddTeacher() {
      // if (
      //   this.teacher.Email &&
      //   this.teacher.EmployeeCode &&
      //   this.teacher.FullName &&
      //   this.teacher.PhoneNumber
      // ) {
      //   this.closeFormTeacher = true;
      // } else if (
      //   !this.teacher.Email &&
      //   !this.teacher.EmployeeCode &&
      //   !this.teacher.FullName &&
      //   !this.teacher.PhoneNumber
      // ) {
      //   this.closeFormTeacher = true;
      // } else {
      //   this.detailPopup.content =
      //     "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không ?";
      //   this.showCancelBtn = true;
      //   this.closePopupNotify = false;
      // }

      if (this.deepEqual(this.prevTeacher, this.teacher)) {
        this.closeFormTeacher = true;
        this.setEmptyTeacher();
        this.setTeacherCurrent({
          id: "",
          name: "",
        });
        this.setEmptyPrevTeacher();
      } else {
        this.detailPopup.content =
          "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không ?";
        this.showCancelBtn = true;
        this.closePopupNotify = false;
      }
    },
    /**
     * Mở form thêm giáo viên
     * Author: Tran Danh (16/7/2022)
     */
    handleOpenFormAddTeacher() {
      this.titleForm = "Thêm";
      this.closeFormTeacher = false;
    },
    /**
     * Mở edit form
     * TRan Danh (25/7/2022)
     */
    handleShowEditForm() {
      this.handleOpenFormAddTeacher();
      this.titleForm = "Sửa";
    },
    /**
     * Khi edit thành công -> chuyển tiêu đề thàng form thêm
     * TRan Danh (25/7/2022)
     */
    handleSuccessEdit() {
      this.titleForm = "Thêm";
    },
    /**
     * click nút more -> hiện ra lựa chọn xóa nhiều
     * Tran Danh (25/7/2022)
     */
    handleToggleDeleteRows() {
      this.isShowDeleteRows = !this.isShowDeleteRows;
    },
    /**
     * Xóa nhiều hàng thông tin teacher
     * Tran Danh (26/7/2022)
     */
    handleDeleteListTeacher() {
      // 1. Khi có chọn các teacherId
      if (this.listTeacherIdDelete.length > 0) {
        this.closePopupNotify = !this.closePopupNotify;
      }
      // 2. Khi không chọn giáo viên nào thì hiện toast cảnh báo
      else {
        this.handleOpenToast();
        this.setDetailToast(
          "Xóa thất bại",
          "Không chọn giáo viên nào!",
          typeToastEnum.FAIL
        );
      }
    },
    /**
     * Mở toast messeger
     * Tran Danh (26/7/2022)
     */
    handleOpenToast() {
      this.isShowToast = true;
    },
    /**
     * Đóng toast messeger
     * Tran Danh (26/7/2022)
     */
    handleCloseToast() {
      this.isShowToast = false;
    },
    /**
     * Hiện toast khi nhấn xóa từng hàng
     * Tran Danh (26/7/2022)
     */
    handleOpenToastOnRow() {
      this.setDetailToast(
        "Thành công",
        "Xóa giáo viên thành công",
        typeToastEnum.SUCCESS
      );
      this.handleOpenToast();
    },
    /**
     * Xét thông tin cho toast messeger
     * Tran Danh (26/7/2022)
     */
    setDetailToast(title, content, type) {
      this.detailToast.titleToast = title;
      this.detailToast.contentToast = content;
      this.detailToast.typeToast = type;
    },
    /**
     * Lọc các teacher phù hợp với từ được tìm kiếm
     * Tran Danh (26/7/2022)
     */
    handleFilter() {
      if (!this.search.content) {
        this.getTeachers(1);
      } else {
        this.search.pageNumber = 1;
        this.setIdxPage(1);
        this.filterTeacher(this.search);
      }
    },

    /**
     * Đóng cả popup và form
     * Tran Danh (29/7/2022)
     */
    handleClosePopupAndForm() {
      this.closeFormTeacher = true;
      this.closePopupNotify = true;
      this.showCancelBtn = false;
    },
    /**
     * Thực hiện validate form khi nhấn Xác nhận ở popup
     * Tran Danh (5/8/2022)
     */
    handleConfirmValidateForm() {
      this.$refs.formAddTeacher?.validateForm();
      this.showCancelBtn = false;
    },
    /**
     * Thực hiện đóng form khi kiểm tra thành công
     * Tran Danh (5/8/2022)
     */
    handleCloseWhenSuccess() {
      this.closeFormTeacher = true;
      this.showCancelBtn = false;
    },

    /**
     * Xét giá trị của index của giá trị tìm kiếm
     * Tran Danh (5/8/2022)
     */
    handleSetIdxWhenChangeSearchText(idx) {
      this.search.pageNumber = idx;
    },
    /**
     * Xuất thông tin giáo viên ra excel
     * Tran Danh (15/8/2022)
     */
    handleExportExcelTeacher() {
      axios({
        url: `${urlTeachers}/Export`,
        method: "GET",
        responseType: "blob",
      }).then((response) => {
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", constants.titleFile);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      });
    },

    /**
     * So sánh 2 object
     * Author: Tran Danh (22/8/2022)
     */
    deepEqual(object1, object2) {
      const keys1 = Object.keys(object1);
      const keys2 = Object.keys(object2);
      if (keys1.length !== keys2.length) {
        return false;
      }
      for (const key of keys1) {
        const val1 = object1[key];
        const val2 = object2[key];
        const areObjects = this.isObject(val1) && this.isObject(val2);
        if (
          (areObjects && !this.deepEqual(val1, val2)) ||
          (!areObjects && val1 !== val2)
        ) {
          return false;
        }
      }
      return true;
    },
    isObject(object) {
      return object != null && typeof object === "object";
    },
    ...mapActions(["deleteListTeacher", "getTeachers", "filterTeacher"]),
  },
  computed: mapGetters(["listTeacherIdDelete", "teacher", "prevTeacher"]),
  watch: {
    // teacher: {
    //   handler(newValue, oldValue) {
    //     if (!this.deepEqual(newValue, oldValue)) {
    //       this.isChangeTeacher = true;
    //     } else {
    //       this.isChangeTeacher = false;
    //     }
    //   },
    //   deep: true,
    // },
  },
};
</script>

<style scoped>
.system-page {
  height: 100%;
  width: 100%;
}

.system-page__control {
  justify-content: space-between;
  margin: 16px 0;
}

.export-icon {
  margin: 0 8px;
}

.system-page__table {
  max-height: calc(
    100vh - (var(--height-input) + 33px + var(--height-pagination))
  );
  height: calc(100% - (var(--height-input) + 33px + var(--height-pagination)));
}

.show-delete-rows {
  position: relative;
}

.show-delete-rows > .delete-rows {
  display: flex;
  position: absolute;
  content: "";
  top: calc(100% + 8px);
  right: 5px;
  padding-left: 11px;
  padding-top: 3px;
  padding-bottom: 3px;
  min-width: 115px;
  border-radius: 4px;
  background-color: #fff;
  box-shadow: 0 0 5px #b1b1b1;
  z-index: 1;
}

.delete-rows {
  display: none;
}

.delete-rows > .icon {
  margin-right: 11px;
}

.delete-rows > .title-icon {
  font-size: 13px;
  font-family: Open Sans SemiBold;
}

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
  font-size: 13px;
  width: 195px;
}

.input-icon__box:focus {
  outline: unset;
  font-family: Open Sans SemiBold;
}

.input-icon__icon {
  padding: 0 8px;
  cursor: pointer;
}
/* v-tooltip */
/* .v-tooltip {
  display: block;
} */
</style>