<template>
  <tr :class="this.teacher.checked && 'checked'">
    <td class="subtitle-one">
      <button
        class="checkbox-input d-flex"
        :class="this.teacher.checked && 'active'"
        v-on:click="handleToggleChecked(teacher.EmployeeId)"
      >
        <img
          src="../../../assets/Icons/ic_Checkbox_Inactive.png"
          alt="checkbox-icon"
          class="checkbox-input__icon"
        />
        <img
          src="../../../assets/Icons/ic_Checkbox_Hover.png"
          alt="checkbox-icon"
          class="checkbox-input__icon-hover"
        />
        <img
          src="../../../assets/Icons/ic_Checkbox_Active.png"
          alt="checkbox-icon"
          class="checkbox-input__icon-checked"
        />
      </button>
    </td>
    <td class="btn regular-text">
      {{ teacher.EmployeeCode }}
    </td>
    <td
      class="btn regular-text name-teacher"
      @click="handleClickEdit(teacher.EmployeeId, teacher)"
    >
      {{ teacher.FullName }}
    </td>
    <td class="btn regular-text">
      {{ teacher.PhoneNumber }}
    </td>
    <td class="subtitle-one">
      {{ teacher.CreatedBy }}
    </td>
    <td class="subtitle-one">
      {{ teacher.PositionName }}
    </td>
    <td class="subtitle-one">
      {{ teacher.DepartmentName }}
    </td>
    <td class="subtitle-one">
      <div class="check" :class="{ show: teacher.training }">
        <img
          src="../../../assets/Icons/ic_Check.png"
          alt="check-icon"
          class="check__icon"
        />
      </div>
    </td>
    <td class="subtitle-one">
      <div class="check" :class="{ show: teacher.jobStatus }">
        <img
          src="../../../assets/Icons/ic_Check.png"
          alt="check-icon"
          class="check__icon"
        />
      </div>
    </td>
    <td class="subtitle-one">
      <div class="edit-and-delete-info-teacher d-flex">
        <div
          class="edit__container fs-28 d-flex show-tooltip"
          @click="handleClickEdit(teacher.EmployeeId, teacher)"
        >
          <img
            src="../../../assets/Icons/ic_Edit.png"
            alt="check-icon"
            class="edit__icon"
          />
          <tooltip-comp :contentTooltip="this.listTooltip.edit" />
        </div>
        <div
          class="delete__container fs-28 d-flex show-tooltip"
          @click="handleClickDelete"
        >
          <img
            src="../../../assets/Icons/ic_Remove.png"
            alt="check-icon"
            class="remove__icon"
          />
          <img
            src="../../../assets/Icons/ic_Remove_Hover.png"
            alt="check-icon"
            class="remove__icon-hover"
          />
          <tooltip-comp :contentTooltip="this.listTooltip.delete" />
        </div>
      </div>
    </td>
  </tr>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
import axios from "axios";
import urlEmployees from "@/scripts/constants/urlTeachers";
import typeToast from "../../../scripts/enum/typeToast";
import TooltipComp from "../tooltip/TooltipComp.vue";
export default {
  name: "RowTableTeachers",
  components: { TooltipComp },
  data() {
    return {
      listTooltip: {
        edit: "S???a",
        delete: "X??a",
      },
      toastInfo: {
        titleToast: "Th??ng b??o",
        contentToast: "X??a th??nh c??ng",
        typeToast: typeToast.SUCCESS,
      },
      // rowChecked: false,
    };
  },
  props: {
    teacher: Object,
  },
  methods: {
    /*
      - K??ch ho???t s??? ki???n `toggelChecked` v?? ?????y d??? li???u l??n 
      -> TableTeachers.vue : nh???n s??? ki???n + d??? li???u -> x??? l??
      - K??ch ho???t s??? ki???n `checkORunchekAuto` 
      -> n??t checkALL auto check ho???c uncheck
      Author: Tran Danh (16/7/2022)
    */
    handleToggleChecked() {
      this.$emit("toggelChecked", this.teacher.EmployeeId);
    },
    /**
     * X??a h??ng ??? trong b???ng
     * Tran Danh (25/7/2022)
     */
    handleClickDelete() {
      this.SET_TEACHER_CURRENT({
        id: this.teacher.EmployeeId,
        name: this.teacher.FullName,
      });
      this.$emit("showPopupNotifyOnRow");
    },
    /**
     * S???a th??ng tin h??ng trong b???ng
     * Tran Danh (215/7/2022)
     */
    handleClickEdit(teacherId, teacher) {
      this.SET_TEACHER_CURRENT({
        id: teacherId,
        name: teacher.FullName,
      });

      axios
        .get(`${urlEmployees}/${teacherId}`)
        .then((res) => {
          // console.log(res);
          this.SET_NEWTEACHER({
            EmployeeCode: res.data.EmployeeCode,
            FullName: res.data.FullName,
            PhoneNumber: res.data.PhoneNumber,
            Email: res.data.Email,
          });
        })
        .catch((err) => {
          console.error(err);
        });

      this.$emit("editTeacherInfo");
    },
    ...mapMutations([
      "SET_TEACHER_CURRENT",
      "SET_NEWTEACHER",
    ]),
  },
  computed: mapGetters(["teacher"]),
};
</script>

<style scoped>
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

tr .check {
  text-align: center;
}

tr:hover {
  background-color: var(--hover-color) !important;
}

tr.checked {
  background-color: var(--row-focus-color-on-grid-color) !important;
}

.name-teacher {
  color: var(--main-color);
}

/* icon */
.edit__container {
  justify-content: center;
}

.edit__container:hover,
.delete__container:hover {
  background-color: var(--bg-icon-hover);
  border-radius: 50%;
}

.regular-text {
  font-family: Open Sans Regular;
}
</style>