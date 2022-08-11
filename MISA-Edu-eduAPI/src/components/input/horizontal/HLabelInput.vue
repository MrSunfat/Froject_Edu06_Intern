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
import { mapGetters, mapMutations } from "vuex";
import constanst from "../../../scripts/constants/constants";
export default {
  name: "HLabelInput",
  props: {
    label: String,
    binding: Boolean,
    tabindex: Number,
    stringRef: String,
  },
  data() {
    return {
      dataIp: "",
      contentError: "",
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
  },
  methods: {
    /**
     * Focus đến ô input đầu tiên
     * Tran Danh (27/7/2022)
     */
    focusInput() {
      // console.log("Hello");
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
          this.dataIp = this.dataIp.toUpperCase();
          // Loại bỏ các khoảng trắng thừa trước khi xét vào teacherCOde
          this.SET_TEACHERCODE(this.dataIp.trim());
          // Xét giá trị cho nội dụng báo lỗi
          this.contentError = `${this.label} không được bỏ trống !`;
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
          this.SET_FULLNAME(this.dataIp);
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
          console.log("Đúng email");
          setData(data);
          this.$emit("hideError", label);
        }
        // 2. Nếu dữ liệu sai format -> hiện lỗi
        else if (!regexpFormat.test(data)) {
          console.log("Sai email");
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
     */
    handleChangeData() {
      console.log("Thay đổi");
      this.dataIp = this.dataIp.trim();
    },
    ...mapMutations([
      "SET_TEACHERCODE",
      "SET_FULLNAME",
      "setPhoneNumber",
      "setEmail",
    ]),
  },
  computed: {
    ...mapGetters([
      "TeacherCode",
      "FullName",
      "PhoneNumber",
      "Email",
      "teacher",
    ]),
    errorNotify() {
      return `${this.label} không được bỏ trống !`;
    },
  },
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

.h-label-input.error .notify {
  --height-notify: 22px;
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
}

.h-label-input.error .arrow-down {
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