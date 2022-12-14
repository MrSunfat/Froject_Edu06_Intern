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
          <div class="input-icon__icon" @click="handleFilter">
            <img :src="urlSeachIcon" alt="" class="input-icon__icon-img" />
          </div>
        </div>
      </div>
      <div class="right d-flex">
        <base-btn
          nameBtn="Thêm"
          :type="typeBtnEnum.PRIMARY"
          :handleClick="handleOpenFormAddTeacher"
          class="show-tooltip"
        >
          <tooltip-comp :contentTooltip="this.listTooltipContent.add" />
        </base-btn>
        <base-btn
          class="export-icon"
          nameBtn="Xuất khẩu"
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
    <footer-bar-pagination :searchText="this.search" />
    <form-add-teacher
      v-if="!this.closeFormTeacher"
      @closeFormAddTeacher="handleCloseFormAddTeacher"
      @successEdit="handleSuccessEdit"
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
    />
    <base-toast
      v-if="isShowToast"
      v-bind="detailToast"
      @closeToast="handleCloseToast"
    />
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
// type enum
import typeBtn from "../../scripts/enum/typeBtn";
import typeToastEnum from "@/scripts/enum/typeToast";
import BaseToast from "../common/toast-message/BaseToast.vue";
import BaseBtn from "../common/button/BaseBtn.vue";
import TableTeachers from "../table/TableTeachers.vue";
import FooterBarPagination from "../footer-bar/FooterBarPagination.vue";
import PopupNotify from "../popup/PopupNotify.vue";
import FormAddTeacher from "../form/FormAddTeacher.vue";
import TooltipComp from "../common/tooltip/TooltipComp.vue";
// icon
import moreIcon from "../../assets/Icons/ic_More.png";
import deleteIcon from "../../assets/Icons/ic_delete.png";
import searchIcon from "../../assets/Icons/Ic_seerch.png";
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
        content: "Bạn có chắc chắn xóa Cán bộ, giáo viên đang chọn không ?",
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
    TooltipComp,
    BaseToast,
  },
  methods: {
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
     * Đóng form thêm giáo viên và
     * Author: Tran Danh (16/7/2022)
     */
    handleCloseFormAddTeacher() {
      console.log(this.teacher);
      if (
        this.teacher.Email &&
        this.teacher.EmployeeCode &&
        this.teacher.FullName &&
        this.teacher.PhoneNumber
      ) {
        this.closeFormTeacher = true;
      } else if (
        !this.teacher.Email &&
        !this.teacher.EmployeeCode &&
        !this.teacher.FullName &&
        !this.teacher.PhoneNumber
      ) {
        this.closeFormTeacher = true;
      } else {
        this.detailPopup.content =
          "Dữ liệu đã bị thay đổi, bạn có muốn lưu lại không ?";
        this.showCancelBtn = true;
        this.closePopupNotify = false;
      }
      this.titleForm = "Thêm";
    },
    /**
     * Mở form thêm giáo viên
     * Author: Tran Danh (16/7/2022)
     */
    handleOpenFormAddTeacher() {
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
      // console.log(this.listTeacherIdDelete);
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
      console.log(this.search);
      if (!this.search) {
        this.getTeachers(1);
      } else {
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
    },
    ...mapActions(["deleteListTeacher", "getTeachers", "filterTeacher"]),
  },
  computed: mapGetters(["listTeacherIdDelete", "teacher"]),
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