<template>
  <tr :class="this.teacher?.checked && 'checked'">
    <td class="subtitle-one">
      <button
        class="checkbox-input d-flex"
        :class="this.teacher?.checked && 'active'"
        v-on:click="handleToggleChecked(teacher?.TeacherID)"
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
      {{ teacher.TeacherCode }}
    </td>
    <td
      class="btn regular-text name-teacher c-p"
      @click="handleClickEdit(teacher.TeacherID, teacher)"
      :title="teacher?.FullName"
    >
      {{ teacher?.FullName }}
    </td>
    <td class="btn regular-text">
      {{ teacher?.PhoneNumber }}
    </td>
    <td class="subtitle-one department-column" :title="teacher?.DepartmentName">
      {{ teacher?.DepartmentName }}
    </td>
    <td class="subtitle-one subjectname-column" :title="teacher?.SubjectName">
      {{ teacher?.SubjectName }}
    </td>
    <td class="subtitle-one room-column" :title="teacher?.Room">
      {{ teacher?.Room }}
    </td>
    <td class="subtitle-one">
      <div
        class="check"
        :class="{ show: teacher?.IsProfessionalQualifications }"
      >
        <img
          src="../../../assets/Icons/ic_Check.png"
          alt="check-icon"
          class="check__icon"
        />
      </div>
    </td>
    <td class="subtitle-one">
      <div class="check" :class="{ show: teacher?.IsWorking }">
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
          @click="handleClickEdit(teacher?.TeacherID, teacher)"
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
import srcAvatar from "@/assets/Icons/avatar-default.jpg";
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
    teacher: {
      type: Object,
    },
  },
  methods: {
    /*
      - K??ch ho???t s??? ki???n `toggelChecked` v?? ?????y d??? li???u l??n 
      -> TableTeachers.vue : nh???n s??? ki???n + d??? li???u -> x??? l??
      - K??ch ho???t s??? ki???n `checkORunchekAuto` 
      -> n??t checkALL auto check ho???c uncheck
      Author: Tran Danh (16/7/2022)
    */
    handleToggleChecked(teacherID) {
      this.$emit("toggelChecked", teacherID);
    },
    /**
     * X??a h??ng ??? trong b???ng
     * Tran Danh (25/7/2022)
     */
    handleClickDelete() {
      this.setTeacherCurrent({
        id: this.teacher.TeacherID,
        name: this.teacher.FullName,
      });
      this.$emit("showPopupNotifyOnRow");
    },
    /**
     * S???a th??ng tin h??ng trong b???ng
     * Tran Danh (215/7/2022)
     */
    handleClickEdit(teacherId, teacher) {
      this.setTeacherCurrent({
        id: teacherId,
        name: teacher.FullName,
      });

      this.showLoading();
      axios
        .get(`${urlEmployees}/${teacherId}`)
        .then((res) => {
          this.setNewTeacher({
            TeacherCode: res.data.TeacherCode,
            FullName: res.data.FullName,
            PhoneNumber: res.data.PhoneNumber,
            Email: res.data.Email,
            Files: res.data.Files,
            ImgByte: res.data.ImgByte,
            AvatarSrc: srcAvatar,
            DepartmentID: res.data.DepartmentID,
            IsProfessionalQualifications: res.data.IsProfessionalQualifications,
            IsWorking: res.data.IsWorking,
            DayOff: res.data.DayOff,
            ListSubject: [...res.data.ListSubject],
            ListRoom: [...res.data.ListRoom],
          });
          if (res.data.ImgByte != null && res.data.ImgByte.length > 0) {
            this.setAvatarSrc("data:image/jpg;base64," + res.data.ImgByte);
          }
          this.setPrevTeacher({
            TeacherCode: res.data.TeacherCode,
            FullName: res.data.FullName,
            PhoneNumber: res.data.PhoneNumber,
            Email: res.data.Email,
            Files: res.data.Files,
            ImgByte: res.data.ImgByte,
            AvatarSrc: res.data.ImgByte
              ? "data:image/jpg;base64," + res.data.ImgByte
              : srcAvatar,
            DepartmentID: res.data.DepartmentID,
            IsProfessionalQualifications: res.data.IsProfessionalQualifications,
            IsWorking: res.data.IsWorking,
            DayOff: res.data.DayOff,
            ListSubject: [...res.data.ListSubject],
            ListRoom: [...res.data.ListRoom],
          });
          this.hideLoading();
        })
        .catch((err) => {
          console.error(err);
        });

      this.$emit("editTeacherInfo");
    },
    /**
     * Ch???nh s???a c??c danh s??ch item th??nh chu???i
     * Author: TNDanh (15/8/2022)
     */
    stringListItems(listItem, prop) {
      return listItem.map((item) => item[prop]);
    },
    ...mapMutations([
      "setTeacherCurrent",
      "setNewTeacher",
      "setAvatarSrc",
      "setPrevTeacher",
      "showLoading",
      "hideLoading",
    ]),
  },
  computed: {
    ...mapGetters(["teacher"]),
  },
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
  min-width: 150px;
  max-width: 205px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
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

.department-column {
  max-width: 128px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.subjectname-column {
  max-width: 98px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.room-column {
  max-width: 182px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
</style>