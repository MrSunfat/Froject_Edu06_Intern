<template>
  <div class="table-teachers">
    <table class="table-teachers__container">
      <tr class="table-teachers__container__title">
        <th class="title">
          <button
            class="checkbox-input d-flex"
            :class="this.checkAll && 'active'"
            v-on:click="handleToggleCheckedAll"
          >
            <img
              src="../../assets/Icons/ic_Checkbox_Inactive.png"
              alt="checkbox-icon"
              class="checkbox-input__icon"
            />
            <img
              src="../../assets/Icons/ic_Checkbox_Hover.png"
              alt="checkbox-icon"
              class="checkbox-input__icon-hover"
            />
            <img
              src="../../assets/Icons/ic_Checkbox_Active.png"
              alt="checkbox-icon"
              class="checkbox-input__icon-checked"
            />
          </button>
        </th>
        <th
          class="title center-text show-tooltip"
          v-for="info in this.infoTeacher"
          :key="info.title"
        >
          {{ info.title }}
          <tooltip-comp
            v-if="info.tooltipContent"
            :contentTooltip="info.tooltipContent"
          />
        </th>
      </tr>
      <row-table-teachers
        v-for="teacher in teachers"
        :key="teacher.code"
        :teacher="teacher"
        @toggelChecked="handleToggleChecked"
        @showPopupNotifyOnRow="handleOpenPopupNotify"
        @editTeacherInfo="handleOpenEditForm"
      />
    </table>
    <loading-comp v-if="isShowLoading" />
    <popup-notify
      v-if="this.isShowPopupNotify"
      @closePopupNotify="handleClosePopupNotify"
      @showToastSuccess="handleShowToast"
    />
  </div>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import RowTableTeachers from "../common/row-table/RowTableTeachers.vue";
import LoadingComp from "../common/loading/LoadingComp.vue";
import PopupNotify from "../popup/PopupNotify.vue";
import TooltipComp from "../common/tooltip/TooltipComp.vue";
export default {
  name: "TableTeachers",
  components: { RowTableTeachers, LoadingComp, PopupNotify, TooltipComp },
  data() {
    return {
      /* 
            Các thông tin của 1 cán bộ, giáo viên
            -> tiêu đề cho các cột
        */
      infoTeacher: [
        {
          title: "Số hiệu cán bộ",
        },
        {
          title: "Họ và tên",
        },
        {
          title: "Số điện thoại",
        },
        {
          title: "Tổ chuyên môn",
        },
        {
          title: "QL theo môn",
          tooltipContent: "Quản lý theo môn",
        },
        {
          title: "QL kho, phòng",
          tooltipContent: "Quản lý kho, phòng",
        },
        {
          title: "Đào tạo QLTB",
          tooltipContent: "Đào tạo quản lý thiết bị",
        },
        {
          title: "Đang làm việc",
        },
        {
          title: "",
        },
      ],
      fakeDbTeacher: [],
      checkAll: false,
      isShowPopupNotify: false,
      isShowToast: false,
    };
  },
  methods: {
    /*
      Xử lý khi nhấn nút check hoặc bỏ check toàn bộ
      Author: Tran Danh (21/7/2022)
    */
    handleToggleCheckedAll() {
      this.checkAll = !this.checkAll;
      this.checkAll
        ? this.setListTeacherIdDelete(
            this.teachers.map((teacher) => teacher.EmployeeId)
          )
        : this.setListTeacherIdDelete([]);
      this.CHECKALL_TEACHERS(this.checkAll);
    },
    /*
      Xử lý sự kiện check của từng hàng trong bảng
      Author: Tran Danh (21/7/2022)
    */
    handleToggleChecked(teacherId) {
      this.CHECK_TEACHER(teacherId);
      // console.log(this.teachers);
      for (let teacher of this.teachers) {
        if (teacher.EmployeeId === teacherId) {
          teacher.checked
            ? this.addListTeacherIdDelete(teacherId)
            : this.removeListTeacherIdDelete(teacherId);
          // console.log(teacherId, teacher);
        }
      }
      this.checkAll = this.isCheckAllTeachers;
    },
    /**
     * Nhận sự kiện từ row table -> Mở popup thông báo lên
     * Author: Tran Danh (19/7/2022)
     */
    handleOpenPopupNotify() {
      this.isShowPopupNotify = true;
    },
    /**
     * Nhận sự kiện từ row table -> Đóng popup thông báo
     * Author: Tran Danh (21/7/2022)
     */
    handleClosePopupNotify() {
      this.isShowPopupNotify = false;
    },
    /**
     * Mở loading và đổ dữ liệu về Table
     * Author: Tran Danh (20/7/2022)
     */
    dumpData() {
      this.SHOW_LOADING();
      this.getTeachers(this.idxPage);
    },
    /**
     * Show popup sửa thông tin lên
     * TRan Danh (25/7/2022)
     */
    handleOpenEditForm() {
      this.$emit("showEditForm");
    },
    /**
     * Hiện toast xóa thành công khi xóa từng hàng
     * Tran Danh (26/7/2022)
     */
    handleShowToast() {
      this.$emit("showToastOnRow");
    },
    ...mapActions(["getTeachers"]),
    ...mapMutations([
      "CHECK_TEACHER",
      "CHECKALL_TEACHERS",
      "SHOW_LOADING",
      "HIDE_LOADING",
      "setListTeacherIdDelete",
      "addListTeacherIdDelete",
      "removeListTeacherIdDelete",
    ]),
  },
  computed: mapGetters([
    "isShowLoading",
    "isCheckAllTeachers",
    "teachers",
    "totalPage",
    "idxPage",
    "teachers",
    "listTeacherIdDelete",
  ]),
  watch: {
    /**
     * Nếu teachers có giá trị -> đóng loading
     * Author: Tran Danh (20/7/2022)
     */
    teachers() {
      this.HIDE_LOADING();
    },
  },
  created() {
    // this.HIDE_LOADING();
    this.dumpData();
  },
};
</script>

<style scoped>
@import url("../../style/components/table/table-teachers.css");
</style>