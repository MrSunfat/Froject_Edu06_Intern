<template>
  <div class="popup-form-add-teacher">
    <div class="form-add-teacher">
      <div class="form-add-teacher__avatar-container">
        <div class="avatar-box__img d-flex">
          <div class="avatar-box-img">
            <img
              :src="srcAvatar"
              alt=""
              accept="image/*"
              class="avatar-img"
              ref="avatar"
            />
          </div>
          <h1
            class="subtitle-two center-text avatar-box__subtitle"
            @click="handleFileInputAvatar"
          >
            Chọn ảnh
          </h1>
          <input
            ref="fileInputAvatar"
            type="file"
            class="file-input-avatar"
            @change="changeAvatar"
          />
        </div>
        <div class="avatar-box__info d-flex">
          <h1 class="title nowrap" :title="this.FullName">
            {{ this.FullName || "Họ và tên" }}
          </h1>
          <p class="subtitle-two" :title="this.TeacherCode">
            {{ this.TeacherCode || "Số hiệu cán bộ" }}
          </p>
        </div>
      </div>
      <div class="form-add-teacher__inputs">
        <h1 class="title">{{ this.titleForm }} hồ sơ Cán bộ, giáo viên</h1>
        <div class="inputs__main">
          <div class="inputs__main__one">
            <h-label-input
              id="test-ip"
              :label="constanst.propertiesTeacher.code"
              :binding="true"
              :tabindex="1"
              stringRef="firstFocus"
              :class="{
                error: this.bindingErrorIP.teacherCode,
              }"
              @hideError="handleHideError"
            />
            <h-label-input
              :label="constanst.propertiesTeacher.fullName"
              :binding="true"
              :tabindex="2"
              stringRef="twoFocus"
              :class="{
                error: this.bindingErrorIP.fullName,
              }"
              @hideError="handleHideError"
            />
            <h-label-input
              :label="constanst.propertiesTeacher.phoneNumber"
              :tabindex="3"
              :class="{
                error: this.bindingErrorIP.phoneNumber,
              }"
              @showError="handleShowError"
              @hideError="handleHideError"
            />
            <h-label-input
              :label="constanst.propertiesTeacher.email"
              :tabindex="4"
              :class="{
                error: this.bindingErrorIP.email,
              }"
              @showError="handleShowError"
              @hideError="handleHideError"
            />
            <drop-down-one
              :label="constanst.propertiesTeacher.subjectGroup"
              :tabindex="5"
              :dbDropdown="dbDropdown"
              @changeOption="handleChangeOption"
            />
            <tag-comp
              :label="constanst.propertiesTeacher.subject"
              :tabindex="6"
              :listSubject="listSubject"
              @selectSubject="handleSelectSubject"
              @isCheckAll="isCheckAll"
              @deleteSubject="deleteSubject"
            />
          </div>
          <div class="inputs__main__two">
            <!-- <drop-down-one /> -->
          </div>
          <tag-comp
            :label="constanst.propertiesTeacher.department"
            :tabindex="7"
            :listSubject="listGroup"
            @selectSubject="handleSelectSubject"
            @isCheckAll="isCheckAll"
            @deleteSubject="deleteSubject"
            typeTag="lab"
          />
          <!-- <drop-down-one /> -->
          <div class="inputs__main__three d-flex">
            <div class="career-level d-flex">
              <button
                class="checkbox-input d-flex none-pd-l mg-r-8"
                tabindex="8"
                :class="this.careerLevel && 'active'"
                v-on:click="handleToggleCareerLevel"
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
              <h1 class="subtitle-two">Trình độ nghiệp vụ QLTB</h1>
            </div>

            <div class="working d-flex mg-l-8">
              <button
                class="checkbox-input d-flex mg-r-8 no-pd-l"
                tabindex="9"
                :class="this.working && 'active'"
                v-on:click="handleToggleWorking"
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
              <h1 class="subtitle-two">Đang làm việc</h1>
            </div>
            <input-date :tabindex="10" class="mg-l-8" v-show="showDayOff" />
          </div>
        </div>
        <div class="inputs__btns">
          <base-btn
            class="inputs__btns__close"
            type="second-btn"
            nameBtn="Đóng"
            tabindex="11"
            :handleClick="handleCloseFormAddTeacher"
          />
          <base-btn
            class="inputs__btns__save mg-l-8"
            type="primary-btn"
            nameBtn="Lưu"
            tabindex="12"
            :handleClick="validateForm"
          />
          <!-- v-on:selectOption="handleSelectOption" -->
        </div>
      </div>
      <div class="form-add-teacher__close" @click="handleCloseFormAddTeacher">
        <img src="../../assets/Icons/ic_X_2.png" alt="" class="close-icon" />
      </div>
    </div>
    <!-- <toast-fail :class="{ show: this.showToast.toastFail }" /> -->
    <loading-comp v-if="this.isShowLoading" />
  </div>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import BaseBtn from "../common/button/BaseBtn.vue";
import DropDownOne from "../input/DropDownOne.vue";
import HLabelInput from "../input/horizontal/HLabelInput.vue";
import InputDate from "../input/InputDate.vue";
import TagComp from "../input/TagComp.vue";
import LoadingComp from "../common/loading/LoadingComp.vue";
// img
import avatarDefault from "../../assets/Icons/ic_Avatar_36.png";
// constanst
import constanst from "../../scripts/constants/constants";
export default {
  name: "FormAddTeacher",
  props: {
    titleForm: {
      type: String,
      default: "Thêm",
    },
  },
  components: {
    HLabelInput,
    DropDownOne,
    InputDate,
    BaseBtn,
    TagComp,
    LoadingComp,
  },
  data() {
    return {
      careerLevel: false,
      working: false,
      showDayOff: true,
      dbDropdown: {
        listSubjectGroup: [
          {
            name: "Tổ toán tin",
            selected: false,
          },
          {
            name: "Tổ hóa lý",
            selected: true,
          },
          {
            name: "Tổ sinh",
            selected: false,
          },
          {
            name: "Tổ sử địa",
            selected: false,
          },
          {
            name: "Tổ văn học",
            selected: false,
          },
        ],
        optionSelected: "",
      },
      showToast: {
        toastSuccess: false,
        toastFail: false,
      },
      listSubject: [
        {
          id: "Math",
          name: "Toán",
          selected: true,
        },
        {
          id: "Physics",
          name: "Vật lý",
          selected: false,
        },
        {
          id: "Chemistry",
          name: "Hóa học",
          selected: true,
        },
        {
          id: "Bio",
          name: "Sinh học",
          selected: true,
        },
        {
          id: "History",
          name: "Lịch sử",
          selected: true,
        },
      ],
      listGroup: [
        {
          id: "math-lab",
          name: "Phòng Toán-Lý",
          selected: false,
        },
        {
          id: "chemistry-lab",
          name: "Phòng Hóa-Sinh",
          selected: true,
        },
        {
          id: "common-room",
          name: "Kho phòng chung",
          selected: true,
        },
      ],
      bindingErrorIP: {
        teacherCode: false,
        fullName: false,
        phoneNumber: false,
        email: false,
      },
      srcAvatar: avatarDefault,
      constanst,
    };
  },
  methods: {
    /*
      Checkbox Trình độ nghiệp vụ QLTB
      Author: Tran Danh (16/7/2022)
    */
    handleToggleCareerLevel() {
      this.careerLevel = !this.careerLevel;
    },
    /*
      Checkbox đang làm việc
      Author: Tran Danh (16/7/2022)
    */
    handleToggleWorking() {
      this.showDayOff = !this.showDayOff;
      this.working = !this.working;
    },
    /*
      Đóng form thêm cán bộ
      Author: Tran Danh (16/7/2022)
    */
    handleCloseFormAddTeacher() {
      this.$emit("closeFormAddTeacher");
      // this.SET_TEACHER_CURRENT({ id: "", name: "" });
      // this.setEmptyTeacher();
    },
    /*
      Xử lý khi lưu dữ liệu
      Author: Tran Danh (16/7/2022)
    */
    validateForm() {
      // I. EmployeeCode trống
      if (!this.TeacherCode) {
        this.bindingErrorIP.teacherCode = true;
        // 1. Kiểm tra FullName
        if (!this.FullName) {
          this.bindingErrorIP.fullName = true;
        } else {
          this.bindingErrorIP.fullName = false;
        }
      }
      // II. EmployeeCode khác trống
      else {
        this.bindingErrorIP.teacherCode = false;
        // 1. Kiểm tra FullName
        if (!this.FullName) {
          this.bindingErrorIP.fullName = true;
        } else {
          this.bindingErrorIP.fullName = false;
          // 1a. kiểm tra các ô khác (nếu có dữ liệu) đúng format chưa
          if (!this.bindingErrorIP.phoneNumber && !this.bindingErrorIP.email) {
            this.SHOW_LOADING();
            this.teacher.id = this.teacherIdCurrent;
            if (this.titleForm === "Thêm") {
              this.addNewTeacher(this.teacher);
            } else {
              this.editTeacherInfo(this.teacher);
              this.$emit("successEdit");
            }

            this.$emit("closeWhenSuccess");
            this.setEmptyTeacher();
          }
        }
      }
    },
    /**
     * Xử lý hiện error khi lỗi format
     * Tran Danh (22/7/2022)
     */
    handleShowError(nameInput) {
      switch (nameInput) {
        // 1. Số điện thoại
        case constanst.propertiesTeacher.phoneNumber:
          this.bindingErrorIP.phoneNumber = true;
          break;
        // 2. Email
        case constanst.propertiesTeacher.email:
          this.bindingErrorIP.email = true;
          break;
      }
    },
    /**
     * Khi nhập dữ liệu required thì tắt error
     * Tran Danh (27/7/2022)
     */
    handleHideError(nameInput) {
      switch (nameInput) {
        // 1. Số hiệu cán bộ
        case constanst.propertiesTeacher.code:
          this.bindingErrorIP.teacherCode = false;
          break;
        // 2. Tên giáo viên
        case constanst.propertiesTeacher.fullName:
          this.bindingErrorIP.fullName = false;
          break;
        // 3. Số điện thoại
        case constanst.propertiesTeacher.phoneNumber:
          this.bindingErrorIP.phoneNumber = false;
          break;
        // 4. Email
        case constanst.propertiesTeacher.email:
          this.bindingErrorIP.email = false;
          break;
      }
      // console.log(event);
    },
    ...mapActions(["addNewTeacher", "getTeachers", "editTeacherInfo"]),
    ...mapMutations([
      "SET_NEWTEACHER",
      "setEmptyTeacher",
      "SET_IDXPAGE",
      "SET_TEACHER_CURRENT",
      "setPhoneNumber",
      "setEmail",
      "SHOW_LOADING",
      "HIDE_LOADING",
    ]),
    /*
      Xử lý khi chọn option khác trong dropdown
      Author: Tran Danh (16/7/2022)
    */
    handleChangeOption(nameOption) {
      for (let subject of this.dbDropdown.listSubjectGroup) {
        subject.selected = subject.name === nameOption;
      }
      this.findOptionSelected();
    },
    /**
     * Tìm tên option được chọn trong 1 list các option
     * Author: Tran Danh (16/7/2022)
     */
    findOptionSelected() {
      for (let i = 0; i < this.dbDropdown.listSubjectGroup.length; i++) {
        if (this.dbDropdown.listSubjectGroup[i].selected) {
          this.dbDropdown.optionSelected =
            this.dbDropdown.listSubjectGroup[i].name;
        }
      }
    },
    /**
     * Chọn avatar
     * Author: TRan Danh (16/7/2022)
     */
    handleFileInputAvatar() {
      this.$refs.fileInputAvatar.click();
    },
    /**
     *  Thay đổi url của avatar
     */
    changeAvatar(event) {
      const file = event.target.files[0];
      this.srcAvatar = URL.createObjectURL(file);
      console.log(this.srcAvatar);
    },
    /**
     * Tag Component
     */
    /**
     * Xử lý khi chọn subject
     */
    handleSelectSubject(idOption) {
      if (idOption.name === "subject") {
        const idxSubject = this.listSubject.findIndex(
          (subject) => subject.id === idOption.id
        );

        if (idxSubject !== -1) {
          this.listSubject[idxSubject].selected =
            !this.listSubject[idxSubject].selected;
        }
      }

      if (idOption.name === "lab") {
        const idxGroup = this.listGroup.findIndex(
          (subject) => subject.id === idOption.id
        );

        if (idxGroup !== -1) {
          this.listGroup[idxGroup].selected =
            !this.listGroup[idxGroup].selected;
        }
      }
    },
    /**
     * Xử lý khi nhấn check all
     * Author: Tran Danh (18/7/2022)
     */
    isCheckAll(checkAllOption) {
      if (checkAllOption.name === "subject") {
        if (!checkAllOption.value) {
          for (let i = 0; i < this.listSubject.length; i++) {
            this.listSubject[i].selected = true;
          }
        } else {
          for (let i = 0; i < this.listSubject.length; i++) {
            this.listSubject[i].selected = false;
          }
        }
      }

      if (checkAllOption.name === "lab") {
        if (!checkAllOption.value) {
          for (let i = 0; i < this.listGroup.length; i++) {
            this.listGroup[i].selected = true;
          }
        } else {
          for (let i = 0; i < this.listGroup.length; i++) {
            this.listGroup[i].selected = false;
          }
        }
      }
    },
    /**
     * Nhận sự kiện từ TagComp
     * -> xét selected của subject đó = false
     * AUthor: Tran Danh (18/7/2022)
     */
    deleteSubject(idSubject) {
      if (idSubject.name === "subject") {
        if (idSubject.value) {
          for (let i = 0; i < this.listSubject.length; i++) {
            if (this.listSubject[i].id === idSubject.value) {
              this.listSubject[i].selected = false;
            }
          }
        } else {
          for (let i = 0; i < this.listSubject.length; i++) {
            this.listSubject[i].selected = false;
          }
        }
      }

      if (idSubject.name === "lab") {
        if (idSubject.value) {
          for (let i = 0; i < this.listGroup.length; i++) {
            if (this.listGroup[i].id === idSubject.value) {
              this.listGroup[i].selected = false;
            }
          }
        } else {
          for (let i = 0; i < this.listGroup.length; i++) {
            this.listGroup[i].selected = false;
          }
        }
      }
    },
  },
  computed: mapGetters([
    "teacher",
    "teachers",
    "TeacherCode",
    "FullName",
    "PhoneNumber",
    "Email",
    "isShowLoading",
    "idxPage",
    "teacherIdCurrent",
  ]),
  watch: {
    teachers() {
      this.HIDE_LOADING();
    },
  },
  mounted() {
    this.findOptionSelected();
  },
};
</script>

<style scoped>
.nowrap {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 100%;
  padding: 0 24px;
  text-align: center;
}

.no-pd-l {
  padding-left: 0;
}

.popup-form-add-teacher {
  position: fixed;
  content: "";
  display: flex;
  align-items: center;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.85);
}

/* .popup-form-add-teacher.show {
  display: flex;
} */

.form-add-teacher {
  position: relative;
  display: grid;
  grid-template-columns: 25% 75%;
  padding-top: 8px;
  padding-bottom: 16px;
  height: 370px;
  width: 900px;
  background-color: #fff;
  margin: 20px auto;
  border-radius: 4px;
}

/* avatar */
.form-add-teacher__avatar-container {
  border-right: 1px solid var(--line-gridpanel-color);
  padding-top: 8px;
}

.avatar-box__img {
  flex-direction: column;
  padding: 0 24px;
}

.avatar-box-img {
  background-color: var(--bg-color);
  height: 180px;
  width: 100%;
}

.avatar-box__subtitle {
  background-color: var(--main-color);
  width: 100%;
  color: #fff;
  border-bottom-left-radius: 4px;
  border-bottom-right-radius: 4px;
  padding-top: 5px;
  padding-bottom: 5px;
  cursor: pointer;
}

.avatar-img {
  width: 100%;
  height: 100%;
}

.avatar-box__info {
  flex-direction: column;
  justify-content: center;
  margin-top: 20px;
}

.avatar-box__info p.subtitle-two {
  margin: 0;
  font-size: 12px;
}

/* inputs */
.form-add-teacher__inputs {
  padding-top: 8px;
  padding-left: 24px;
  padding-right: 24px;
  color: var(--second-color-in-form);
}

.form-add-teacher__inputs > h1.title {
  color: var(--main-color-in-form);
  font-size: 20px;
}

.inputs__main {
  width: 100%;
  margin-top: 28px;
}

.inputs__main__one,
.inputs__main__two {
  display: grid;
  grid-template-columns: 6fr 6fr;
  grid-column-gap: 24px;
  min-width: 100%;
  max-width: min-content;
}

.inputs__main__three {
  width: 100%;
  min-height: 32px;
}

.working {
  margin-left: 20px;
}

/* btn */
.inputs__btns {
  text-align: right;
  margin-top: 25px;
}

.inputs__btns__save {
}

.form-add-teacher__close {
  position: absolute;
  content: "";
  top: 6px;
  right: 6px;
  cursor: pointer;
}

.file-input-avatar {
  display: none;
}

.checkbox-input {
  padding-right: 0;
}
</style>