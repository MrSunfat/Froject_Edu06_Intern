<template>
  <div class="input-date d-flex">
    <label for="day-off" class="subtitle-two d-flex">Ngày nghỉ việc</label>
    <div class="input-date__main d-flex">
      <input
        id="day-off"
        class="main__box"
        type="date"
        placeholder="DD/MM/YYYY"
        v-model="time1"
        min="1997-01-01"
        max="2030-12-31"
        :tabindex="tabindex"
        @change="handleChangeDate"
      />
    </div>
  </div>
</template>

<script>
import { mapMutations, mapGetters } from "vuex";
export default {
  name: "InputDate",
  props: {
    tabindex: {
      type: Number,
      default: 0,
    },
  },
  components: {},
  data() {
    return {
      time1: null,
    };
  },
  methods: {
    ...mapMutations(["setDayOff"]),
    /**
     * Xét giá trị dayoff
     * Author: Tran Danh (16/8/2022)
     */
    handleChangeDate() {
      var date = new Date(this.time1);
      var day = date.getDate(); // yields date
      var month = date.getMonth() + 1; // yields month (add one as '.getMonth()' is zero indexed)
      var year = date.getFullYear(); // yields year
      var hour = date.getHours(); // yields hours
      var minute = date.getMinutes(); // yields minutes
      var second = date.getSeconds(); // yields seconds

      // After this construct a string with the above results as below
      var time =
        day +
        "/" +
        month +
        "/" +
        year +
        " " +
        hour +
        ":" +
        minute +
        ":" +
        second;

      this.setDayOff(time);
    },
  },
  computed: {
    ...mapGetters(["IsWorking"]),
  },
  watch: {
    /**
     * Xét ngày nghỉ null nếu còn đang làm việc
     * Author: Tran Danh (16/8/2022)
     */
    IsWorking() {
      if (this.IsWorking) {
        this.setDayOff("");
      }
    },
  },
};
</script>

<style scoped>
.input-date {
  height: var(--height-input);
  margin-left: auto;
}

.input-date > label {
  color: var(--label-input-color);
}

.input-date__main {
  margin-left: 12px;
  width: 180px;
}

.input-date__main .main__box {
  height: 32px;
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 0;
  padding-left: 12px;
  cursor: pointer;
  width: 100%;
}

.input-date__main .main__box:focus {
  outline: unset;
  border: 1px solid var(--active-border-input);
}

.input-date__main .main__box::-webkit-datetime-edit-text {
  color: #555555;
  /* margin-left: 12px; */
}

.input-date__main .main__box::-webkit-calendar-picker-indicator {
  background: url("../../../assets/Icons/ic_sprites.svg") no-repeat -904px -112px;
  color: rgba(204, 204, 204, 0);
  border-left: 1px solid #ccc;
  padding: 0 12px;
  height: 100%;
  cursor: pointer;
}
</style>