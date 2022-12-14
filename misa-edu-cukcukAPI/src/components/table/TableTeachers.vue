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
            C??c th??ng tin c???a 1 c??n b???, gi??o vi??n
            -> ti??u ????? cho c??c c???t
        */
      infoTeacher: [
        {
          title: "S??? hi???u c??n b???",
        },
        {
          title: "H??? v?? t??n",
        },
        {
          title: "S??? ??i???n tho???i",
        },
        {
          title: "T??? chuy??n m??n",
        },
        {
          title: "QL theo m??n",
          tooltipContent: "Qu???n l?? theo m??n",
        },
        {
          title: "QL kho, ph??ng",
          tooltipContent: "Qu???n l?? kho, ph??ng",
        },
        {
          title: "????o t???o QLTB",
          tooltipContent: "????o t???o qu???n l?? thi???t b???",
        },
        {
          title: "??ang l??m vi???c",
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
      X??? l?? khi nh???n n??t check ho???c b??? check to??n b???
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
      X??? l?? s??? ki???n check c???a t???ng h??ng trong b???ng
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
     * Nh???n s??? ki???n t??? row table -> M??? popup th??ng b??o l??n
     * Author: Tran Danh (19/7/2022)
     */
    handleOpenPopupNotify() {
      this.isShowPopupNotify = true;
    },
    /**
     * Nh???n s??? ki???n t??? row table -> ????ng popup th??ng b??o
     * Author: Tran Danh (21/7/2022)
     */
    handleClosePopupNotify() {
      this.isShowPopupNotify = false;
    },
    /**
     * M??? loading v?? ????? d??? li???u v??? Table
     * Author: Tran Danh (20/7/2022)
     */
    dumpData() {
      this.SHOW_LOADING();
      this.getTeachers(this.idxPage);
    },
    /**
     * Show popup s???a th??ng tin l??n
     * TRan Danh (25/7/2022)
     */
    handleOpenEditForm() {
      this.$emit("showEditForm");
    },
    /**
     * Hi???n toast x??a th??nh c??ng khi x??a t???ng h??ng
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
     * N???u teachers c?? gi?? tr??? -> ????ng loading
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