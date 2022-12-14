<template>
  <div class="h-label-input" :class="{ binding: binding }">
    <div class="notify d-flex">{{ this.contentError || errorNotify }}</div>
    <div class="arrow-down"></div>
    <label :for="label" class="subtitle-two d-flex">
      {{ label }}&nbsp;<span style="color: red">*</span>
    </label>
    <input
      :id="label"
      :tabindex="tabindex"
      type="text"
      placeholder=""
      class="ip-default h-label-input__main"
      :ref="stringRef"
      v-model="dataIp"
      @input="handleEnterData"
    />
  </div>
</template>

<script>
import axios from "axios";
import { mapGetters, mapMutations } from "vuex";
import constanst from "@/scripts/constants/constants";
import urlTeachers from "@/scripts/constants/urlTeachers";
export default {
  name: "HLabelInput",
  props: {
    label: String,
    binding: Boolean,
    tabindex: Number,
    stringRef: String,
    duplicate: String,
    titleForm: String,
  },
  data() {
    return {
      dataIp: "",
      contentError: "",
      newCode: "",
    };
  },
  mounted() {
    /**
     * Lúc nó đc tạo ra kiểm tra xem là ô đầu tiên không
     * Tran Danh (27/7/2022)
     */
    if (this.tabindex === 1) {
      this.focusInput();
    }

    // Nếu thẻ label là Số hiệu cán bộ
    if (
      this.label === constanst.propertiesTeacher.code &&
      this.titleForm === "Thêm"
    ) {
      this.generateNewTeacherCode();
    }
  },
  methods: {
    /**
     * Focus đến ô input đầu tiên
     * Tran Danh (27/7/2022)
     */
    focusInput() {
      this.$refs.firstFocus.focus();
    },
    /**
     * Xét giá trị thích hợp với giá trị sẽ nhận của nó
     * Author: Tran Danh (25/7/2022)
     */
    handleEnterData() {
      switch (this.label) {
        // 1. Xử lý ô nhập Số hiệu cán bộ
        case constanst.propertiesTeacher.code:
          // this.dataIp = this.newCode;
          this.dataIp = this.dataIp.toUpperCase();
          // Loại bỏ các khoảng trắng thừa trước khi xét vào teacherCOde
          this.setTeacherCode(this.dataIp.trim());
          // Xét giá trị cho nội dụng báo lỗi
          this.contentError = `${this.label} không được bỏ trống !`;
          // Kiểm tra mã giáo viên có bị trùng không ?
          if (this.duplicate) {
            this.contentError = this.duplicate;
          }
          // Khi đã nhập thông tin vào ô thì ẩn thông báo lỗi đi
          if (this.dataIp) {
            this.$emit("hideError", constanst.propertiesTeacher.code);
          }
          break;
        // 2. Xử lý ô nhập tên giáo viên
        case constanst.propertiesTeacher.fullName: {
          // Format lại tên giáo viên đúng quy tắc
          let words = this.dataIp.split(" ");
          words = words.map((item) => {
            if (item) {
              return item.charAt(0).toUpperCase() + item.slice(1);
            }
          });
          this.dataIp = words.join(" ");
          this.setFullName(this.dataIp);
          // Xét giá trị cho nội dụng báo lỗi
          this.contentError = `${this.label} không được bỏ trống !`;
          // Khi đã nhập thông tin vào ô thì ẩn thông báo lỗi đi
          if (this.dataIp) {
            this.$emit("hideError", constanst.propertiesTeacher.fullName);
          }
          break;
        }
        // 3. Xử lý ô nhập số điện thoại có chuẩn format ko
        case constanst.propertiesTeacher.phoneNumber:
          this.handleFormatInput(
            this.dataIp,
            constanst.regexpFormat.phone,
            this.setPhoneNumber,
            constanst.propertiesTeacher.phoneNumber
          );
          break;
        // 4. Xử lý ô nhập email có chuẩn format ko
        case constanst.propertiesTeacher.email:
          this.handleFormatInput(
            this.dataIp,
            constanst.regexpFormat.email,
            this.setEmail,
            constanst.propertiesTeacher.email
          );
          break;
      }
    },
    /**
     * Format ô dữ liệu
     * Tran Danh (27/7/2022)
     */
    handleFormatInput(data, regexpFormat, setData, label) {
      // I. Khi nhập dữ liệu
      if (data) {
        // 1. Nếu dữ liệu đúng format -> xét vào data của vuex
        if (regexpFormat.test(data)) {
          setData(data);
          this.$emit("hideError", label);
        }
        // 2. Nếu dữ liệu sai format -> hiện lỗi
        else if (!regexpFormat.test(data)) {
          // Xét giá trị cho nội dụng báo lỗi
          this.contentError = `${label} không đúng định dạng !`;
          this.$emit("showError", label);
        }
        // II. Khi ô dữ liệu trống
      } else {
        this.$emit("hideError", label);
      }
    },
    /**
     * Khi tab hoặc enter ô dữ liệu thì format lại dữ liệu đã nhập
     * Tran Danh (17/8/2022)
     */
    handleChangeData() {
      this.dataIp = this.dataIp.trim();
    },
    /**
     * Tự sinh ra mã giáo viên mới
     * Tran Danh (17/8/2022)
     */
    async generateNewTeacherCode() {
      let res = await axios.get(`${urlTeachers}/NewTeacherCode`);
      this.dataIp = res?.data;
      this.setTeacherCode(this.dataIp);
      this.setPrevTeacherCode(this.dataIp);
      this.$emit("hideError", constanst.propertiesTeacher.code);
    },
    ...mapMutations([
      "setTeacherCode",
      "setFullName",
      "setPhoneNumber",
      "setEmail",
      "setPrevTeacherCode",
    ]),
  },
  computed: {
    ...mapGetters([
      "TeacherCode",
      "FullName",
      "PhoneNumber",
      "Email",
      "teacher",
      "isTeacherCodeDuplicate",
    ]),
    errorNotify() {
      return `${this.label} không được bỏ trống !`;
    },
  },
  created() {},
  watch: {
    // Xét giá trị cho các ô khi muốn sửa giáo viên
    teacher() {
      switch (this.label) {
        case constanst.propertiesTeacher.code:
          this.dataIp = this.TeacherCode;
          break;
        case constanst.propertiesTeacher.fullName:
          this.dataIp = this.FullName;
          break;
        case constanst.propertiesTeacher.phoneNumber:
          this.dataIp = this.PhoneNumber;
          break;
        case constanst.propertiesTeacher.email:
          this.dataIp = this.Email;
          break;
      }
    },
  },
};
</script>

<style scoped>
.h-label-input {
  position: relative;
  display: grid;
  grid-template-columns: var(--width-title-h-input) calc(
      100% - var(--width-title-h-input)
    );
  margin-bottom: 18px;
  min-width: 100%;
  height: var(--height-input);
}

.h-label-input.error:hover .notify {
  --height-notify: 32px;
  padding-left: 8px;
  position: absolute;
  content: "";
  top: calc(-5px - var(--height-notify));
  left: 0;
  right: 0;
  background-color: var(--hover-delete-icon-color);
  color: #fff;
  /* text-align: center; */
  border-radius: 4px;
  height: var(--height-notify);
  font-size: 12px;
  /* align-content: center; */
  justify-content: center;
  display: flex;
  transition: all 0.3s ease-in;
}

.h-label-input.error:hover .arrow-down {
  position: absolute;
  top: -5px;
  right: 50px;
  width: 0;
  height: 0;
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  border-top: 5px solid var(--hover-delete-icon-color);
  display: block;
}

.h-label-input .notify {
  display: none;
}

.h-label-input .arrow-down {
  display: none;
}

.h-label-input h1 {
  line-height: 0;
}

.h-label-input span {
  display: none;
}

.h-label-input.binding span {
  display: block;
}

.h-label-input.binding h1 {
  line-height: 100%;
}

.h-label-input > label {
  color: var(--label-input-color);
}

.h-label-input.error > .h-label-input__main {
  border-color: var(--hover-delete-icon-color);
}

.h-label-input__main {
  min-width: 135px;
  margin-left: 12px;
  padding: 0 12px;
}

.h-label-input__main:focus {
  border-color: var(--active-border-input);
}
</style>