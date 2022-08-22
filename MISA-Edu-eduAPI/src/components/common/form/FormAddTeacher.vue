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
            accept="image/*"
            @change="changeAvatar"
          />
        </div>
        <div class="avatar-box__info d-flex">
          <h1 class="title nowrap" :title="this.FullName">
            {{ this.FullName || constanst.propertiesTeacher.fullName }}
          </h1>
          <p class="subtitle-two nowrap" :title="this.TeacherCode">
            {{ this.TeacherCode || constanst.propertiesTeacher.code }}
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
              ref="first-focus"
              :class="{
                error: this.bindingErrorIP.teacherCode,
              }"
              :duplicate="constanst.warningContent.codeDuplicate"
              :titleForm="this.titleForm"
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
            <!-- <drop-down-one
              :label="constanst.propertiesTeacher.subjectGroup"
              :tabindex="5"
              :dbDropdown="dbDropdown"
              @changeOption="handleChangeOption"
            /> -->
            <div class="dropdown d-flex">
              <label class="subtitle-two d-flex dropdown__label">
                Tổ bộ môn
              </label>
              <DxSelectBox
                class="ip-default dropdown__container mw-190 d-flex"
                ref="ms-selectbox"
                :search-enabled="true"
                :items="dbDropdown.listSubjectGroup"
                display-expr="DepartmentName"
                value-expr="DepartmentID"
                v-model:value="departmentModel"
                placeholder=""
                noDataText="Không tìm thấy tổ hợp môn phù hợp !"
                :tabIndex="5"
              />
            </div>
            <div class="dropdown d-flex">
              <label
                class="subtitle-two d-flex dropdown__label"
                title="Quản lý theo môn"
              >
                QL theo môn
              </label>
              <DxTagBox
                class="ip-default dropdown__container d-flex"
                :search-enabled="true"
                :items="dbDropdown.listSubject"
                :show-selection-controls="true"
                :max-displayed-tags="3"
                display-expr="SubjectName"
                value-expr="SubjectID"
                placeholder=""
                selectAllText="Tất cả"
                v-model:value="listSubjectModel"
                :showDropDownButton="true"
                :multiline="false"
                @multiTagPreparing="onMultiTagPreparingDepartment"
                noDataText="Không tìm thấy tổ hợp môn phù hợp !"
                :tabIndex="6"
              />
            </div>
          </div>
          <div class="inputs__main__two"></div>
          <div class="dropdown d-flex">
            <label
              class="subtitle-two d-flex dropdown__label"
              title="Quản lý kho, phòng"
            >
              QL kho, phòng
            </label>
            <DxTagBox
              class="ip-default dropdown__container d-flex"
              :search-enabled="true"
              :items="dbDropdown.listRoom"
              :show-selection-controls="true"
              :max-displayed-tags="3"
              display-expr="EquimentRoomName"
              value-expr="EquimentRoomID"
              placeholder=""
              selectAllText="Tất cả"
              v-model:value="listRoomModel"
              :showDropDownButton="true"
              :multiline="false"
              @multiTagPreparing="onMultiTagPreparingRoom"
              noDataText="Không tìm thấy tổ hợp môn phù hợp !"
              :tabIndex="7"
            />
          </div>
          <!-- <tag-comp
            :label="constanst.propertiesTeacher.department"
            :tabindex="7"
            :listSubject="listGroup"
            @selectSubject="handleSelectSubject"
            @isCheckAll="isCheckAll"
            @deleteSubject="deleteSubject"
            typeTag="lab"
          /> -->

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
              <h1 class="subtitle-two subtitle-color">
                Trình độ nghiệp vụ QLTB
              </h1>
            </div>

            <div class="working d-flex mg-l-8">
              <button
                class="checkbox-input d-flex mg-r-8 no-pd-l"
                tabindex="9"
                :class="this.working && 'active'"
                v-on:click="handleToggleWorking"
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
              <h1 class="subtitle-two subtitle-color">Đang làm việc</h1>
            </div>
            <div class="input-date d-flex" v-if="!this.working">
              <label for="day-off" class="subtitle-two d-flex subtitle-color"
                >Ngày nghỉ việc</label
              >
              <div class="input-date__main d-flex">
                <DxDateBox
                  :tabIndex="10"
                  type="date"
                  display-format="dd/MM/yyyy"
                  :use-mask-behavior="true"
                  placeholder="dd/mm/yyyy"
                />
              </div>
            </div>
            <!-- <input-date :tabindex="10" class="mg-l-8" v-if="!this.working" /> -->
          </div>
        </div>
        <div class="inputs__btns">
          <base-btn
            class="inputs__btns__close"
            type="second-btn"
            nameBtn="Đóng"
            tabindex="12"
            :handleClick="handleCloseFormAddTeacher"
          />
          <base-btn
            class="inputs__btns__save mg-l-8"
            type="primary-btn"
            nameBtn="Lưu"
            tabindex="11"
            :handleClick="validateForm"
          />
          <!-- v-on:selectOption="handleSelectOption" -->
        </div>
      </div>
      <div class="form-add-teacher__close" @click="handleCloseFormAddTeacher">
        <img :src="closeIcon" alt="" class="close-icon" />
      </div>
    </div>
    <!-- <toast-fail :class="{ show: this.showToast.toastFail }" /> -->
    <loading-comp v-if="this.isShowLoading" />
  </div>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import BaseBtn from "@/components/common/button/BaseBtn.vue";
import HLabelInput from "@/components/common/input/horizontal/HLabelInput.vue";
//import InputDate from "@/components/common/input/InputDate.vue";
import LoadingComp from "@/components/common/loading/LoadingComp.vue";
// img
import avatarDefault from "@/assets/Icons/avatar-default.jpg";
import checkboxInactive from "@/assets/Icons/ic_Checkbox_Inactive.png";
import checkboxHover from "@/assets/Icons/ic_Checkbox_Hover.png";
import checkboxActive from "@/assets/Icons/ic_Checkbox_Active.png";
import closeIcon from "@/assets/Icons/ic_X_2.png";
import DxSelectBox from "devextreme-vue/select-box";
import DxTagBox from "devextreme-vue/tag-box";
import DxDateBox from "devextreme-vue/date-box";

// constanst
import constanst from "@/scripts/constants/constants";
import axios from "axios";
import urlTeachers from "@/scripts/constants/urlTeachers";
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
    // InputDate,
    BaseBtn,
    LoadingComp,
    DxSelectBox,
    DxTagBox,
    DxDateBox,
  },
  data() {
    return {
      file: "",
      checkboxInactive,
      checkboxHover,
      checkboxActive,
      closeIcon,
      careerLevel: false,
      working: false,
      dbDropdown: {
        listSubjectGroup: [
          {
            DepartmentID: 1,
            DepartmentName: "Tổ Sử - Địa - GDCD",
          },
          {
            DepartmentID: 2,
            DepartmentName: "Tổ Toán - Tin",
          },
          {
            DepartmentID: 3,
            DepartmentName: "Tổ Thể Dục - Âm nhạc - Mĩ thuật",
          },
          {
            DepartmentID: 4,
            DepartmentName: "Tổ Anh Văn",
          },
          {
            DepartmentID: 5,
            DepartmentName: "Tổ Ngữ văn",
          },
          {
            DepartmentID: 6,
            DepartmentName: "Tổ Lý - Công nghệ",
          },
          {
            DepartmentID: 7,
            DepartmentName: "Tổ Hóa - Sinh",
          },
          {
            DepartmentID: 8,
            DepartmentName: "P.Văn phòng",
          },
          {
            DepartmentID: 9,
            DepartmentName: "Văn phòng",
          },
          {
            DepartmentID: 10,
            DepartmentName: "Phòng ăn trưa",
          },
        ],
        listSubject: [
          {
            SubjectID: 1,
            SubjectName: "Giáo dục CD",
          },
          {
            SubjectID: 2,
            SubjectName: "Mỹ thuật",
          },
          {
            SubjectID: 3,
            SubjectName: "Ngoại ngữ",
          },
          {
            SubjectID: 4,
            SubjectName: "Vật lý",
          },
          {
            SubjectID: 5,
            SubjectName: "Toán",
          },
          {
            SubjectID: 6,
            SubjectName: "Sinh học",
          },
          {
            SubjectID: 7,
            SubjectName: "Hóa học",
          },
          {
            SubjectID: 8,
            SubjectName: "Ngữ văn",
          },
          {
            SubjectID: 9,
            SubjectName: "Địa lý",
          },
          {
            SubjectID: 10,
            SubjectName: "Thể dục",
          },
          {
            SubjectID: 11,
            SubjectName: "Lịch sử",
          },
          {
            SubjectID: 12,
            SubjectName: "Âm nhạc (Cổ điển)",
          },
          {
            SubjectID: 13,
            SubjectName: "Cổ cầm",
          },
        ],
        listRoom: [
          {
            EquimentRoomID: 1,
            EquimentRoomName: "Kho phòng chung",
          },
          {
            EquimentRoomID: 2,
            EquimentRoomName: "Phòng âm nhạc mỹ thuật",
          },
          {
            EquimentRoomID: 3,
            EquimentRoomName: "Phòng Hóa Sinh",
          },
          {
            EquimentRoomID: 4,
            EquimentRoomName: "Phòng Toán lý",
          },
          {
            EquimentRoomID: 5,
            EquimentRoomName: "Phòng Sử Địa GDCD",
          },
          {
            EquimentRoomID: 6,
            EquimentRoomName: "Phòng Tin học",
          },
          {
            EquimentRoomID: 7,
            EquimentRoomName: "Phòng Máy tính",
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
          selected: false,
        },
        {
          id: "Physics",
          name: "Vật lý",
          selected: false,
        },
        {
          id: "Chemistry",
          name: "Hóa học",
          selected: false,
        },
        {
          id: "Bio",
          name: "Sinh học",
          selected: false,
        },
        {
          id: "History",
          name: "Lịch sử",
          selected: false,
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
          selected: false,
        },
        {
          id: "common-room",
          name: "Kho phòng chung",
          selected: false,
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
      departmentModel: "",
      listSubjectModel: [],
      listRoomModel: [],

      onMultiTagPreparingDepartment: (args) => {
        let itemSize = args.selectedItems.length;
        let totalCount = this.dbDropdown.listSubject.length;
        if (itemSize < totalCount) {
          args.cancel = true;
        } else {
          args.text = "Tất cả";
        }
      },
      onMultiTagPreparingRoom: (args) => {
        let itemSize = args.selectedItems.length;
        let totalCount = this.dbDropdown.listRoom.length;
        if (itemSize < totalCount) {
          args.cancel = true;
        } else {
          args.text = "Tất cả";
        }
      },
    };
  },
  methods: {
    /*
      Checkbox Trình độ nghiệp vụ QLTB
      Author: Tran Danh (16/7/2022)
    */
    handleToggleCareerLevel() {
      this.careerLevel = !this.careerLevel;
      this.setIsProfessionalQualifications(this.careerLevel);
    },
    /*
      Checkbox đang làm việc
      Author: Tran Danh (16/7/2022)
    */
    handleToggleWorking() {
      this.working = !this.working;
      this.setIsWorking(this.working);
    },
    /*
      Đóng form thêm cán bộ
      Author: Tran Danh (16/7/2022)
    */
    handleCloseFormAddTeacher() {
      this.$emit("closeFormAddTeacher");
      this.teacher.DepartmentID = 1;
    },
    /*
      Xử lý khi lưu dữ liệu
      Author: Tran Danh (16/7/2022)
    */
    async validateForm() {
      // I. EmployeeCode trống
      if (!this.TeacherCode) {
        this.bindingErrorIP.teacherCode = true;
        // 1. Kiểm tra FullName
        if (!this.FullName) {
          this.bindingErrorIP.fullName = true;
        } else {
          this.bindingErrorIP.fullName = false;
        }
        this.$refs["first-focus"]?.focusInput();
      }
      // II. EmployeeCode khác trống
      else {
        this.bindingErrorIP.teacherCode = false;
        // 1. Kiểm tra FullName
        if (!this.FullName) {
          this.bindingErrorIP.fullName = true;
          this.$refs["first-focus"]?.focusInput();
        } else {
          this.bindingErrorIP.fullName = false;
          // 1a. kiểm tra các ô khác (nếu có dữ liệu) đúng format chưa
          if (!this.bindingErrorIP.phoneNumber && !this.bindingErrorIP.email) {
            this.showLoading();
            this.teacher.id = this.teacherIdCurrent;
            //this.teacher.TeacherID = this.teacherIdCurrent;
            // Kiểm tra đây là thêm hay sửa giáo viên
            let formData = new FormData();
            for (let key in this.teacher) {
              formData.append(key, this.teacher[key]);
            }
            if (this.file) {
              formData.append("Files", this.file);
            }
            console.log(formData);
            // 1. Thêm giáo viên
            if (this.titleForm === "Thêm") {
              // Kiểm tra trùng mã giáo viên
              await this.addNewTeacher(formData);
              // 1. Bị trùng -> hiện ra thông báo lỗi
              if (this.isTeacherCodeDuplicate) {
                this.bindingErrorIP.teacherCode = true;
                this.hideLoading();
              }
              // 2. Không trùng -> đóng form
              else {
                this.getTeachers(1);
                this.$emit("closeWhenSuccess");
                this.setEmptyTeacher();
              }
            }
            // 2. Sửa thông tin giáo viên
            else {
              this.editTeacherInfo(this.teacher);
              // Upload ảnh avatar mới khi chỉnh sửa
              await axios
                .post(`${urlTeachers}/Upload`, formData, {
                  headers: {
                    "Content-Type": "application/json",
                  },
                })
                .catch((err) => console.log(err));
              this.$emit("successEdit");
              this.$emit("closeWhenSuccess");
              this.setEmptyTeacher();
              this.setTeacherCurrent({
                id: "",
                name: "",
              });
            }
            // Tắt thông báo mã teacherCode trùng đi
            this.setIsTeacherCodeDuplicate(false);
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
    },
    /**
     * Chọn avatar
     * Author: Tran Danh (16/7/2022)
     */
    handleFileInputAvatar() {
      this.$refs.fileInputAvatar.click();
    },
    /**
     *  Thay đổi url của avatar
     *  Tran Danh (27/7/2022)
     */
    changeAvatar(event) {
      this.file = event.target.files[0];
      this.srcAvatar = URL.createObjectURL(this.file);
    },
    /**
     * Tag Component
     */
    /**
     * Xử lý khi chọn subject
     * Author: Tran Danh (18/7/2022)
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
    ...mapActions(["addNewTeacher", "getTeachers", "editTeacherInfo"]),
    ...mapMutations([
      "setNewTeacher",
      "setEmptyTeacher",
      "setIdxPage",
      "setTeacherCurrent",
      "setPhoneNumber",
      "setEmail",
      "setDepartmentID",
      "setListSubject",
      "setListRoom",
      "setIsTeacherCodeDuplicate",
      "showLoading",
      "hideLoading",
      "setIsWorking",
      "setIsProfessionalQualifications",
    ]),
  },
  computed: mapGetters([
    "teacher",
    "teachers",
    "TeacherCode",
    "FullName",
    "PhoneNumber",
    "Email",
    "AvatarSrc",
    "DepartmentID",
    "ListSubject",
    "ListRoom",
    "IsProfessionalQualifications",
    "IsWorking",
    "isShowLoading",
    "idxPage",
    "teacherIdCurrent",
    "isTeacherCodeDuplicate",
  ]),
  watch: {
    teachers() {
      this.hideLoading();
    },
    departmentModel() {
      this.setDepartmentID(this.departmentModel);
    },
    listSubjectModel() {
      this.setListSubject(
        this.listSubjectModel?.map((s) => ({
          SubjectID: s,
          SubjectName: this.dbDropdown.listSubject[s - 1].SubjectName,
        }))
      );
    },
    listRoomModel() {
      this.setListRoom(
        this.listRoomModel?.map((r) => ({
          EquimentRoomID: r,
          EquimentRoomName: this.dbDropdown.listRoom[r - 1]?.EquimentRoomName,
        }))
      );
    },
    DepartmentID() {
      this.departmentModel = this.DepartmentID;
    },
    ListSubject() {
      this.listSubjectModel = this.ListSubject?.map((s) => s.SubjectID);
    },
    ListRoom() {
      this.listRoomModel = this.ListRoom?.map((r) => r.EquimentRoomID);
    },
    IsProfessionalQualifications() {
      this.careerLevel = this.IsProfessionalQualifications;
    },
    IsWorking() {
      this.working = this.IsWorking;
    },
    AvatarSrc() {
      if (this.AvatarSrc) {
        this.srcAvatar = this.AvatarSrc;
      }
    },
  },
  mounted() {
    //this.findOptionSelected();
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
  line-height: 26px;
}

.title-name {
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
  grid-template-columns: 6fr 50%;
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

.checkbox-input:focus {
  outline: unset;
}

.checkbox-input:focus .checkbox-input__icon {
  display: none;
}

.checkbox-input:focus .checkbox-input__icon-hover {
  display: block;
}

.checkbox-input.active:focus .checkbox-input__icon-hover {
  display: none;
}

/* Dropdown  */
.dropdown {
  display: grid;
  grid-template-columns: var(--width-title-h-input) calc(
      100% - var(--width-title-h-input)
    );
  position: relative;
  margin-bottom: 18px;
}

.dropdown .dropdown__container {
  margin-left: 12px;
  border-color: #ccc;
}

.dropdown__label {
  color: #757575;
}

.mw-190 {
  max-width: 190px !important;
}

.input-date > label {
  margin-left: 13px;
  margin-right: 8px;
}

.input-date__main {
  width: 200px !important;
  height: 32px;
  /* border-color: #ccc !important; */
}

/* .dx-texteditor.dx-editor-outlined {
  border-color: #ccc !important;
} */

.dx-calendar-cell.dx-calendar-selected-date.dx-calendar-contoured-date,
.dx-calendar-cell.dx-calendar-selected-date.dx-calendar-today.dx-calendar-contoured-date {
  /* -webkit-box-shadow: inset 0 0 0 1px #bebebe, inset 0 0 0 1000px #337ab7; */
  box-shadow: inset 0 0 0 1px #bebebe, inset 0 0 0 1000px #03ae66 !important;
}

.dx-datebox {
  height: 32px;
  border-color: #ccc !important;
}

.dx-texteditor.dx-state-focused.dx-editor-outlined {
  border-color: #03ae66 !important;
}

.subtitle-color {
  color: #333;
}
</style>