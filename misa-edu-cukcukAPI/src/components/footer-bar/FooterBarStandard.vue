<template>
  <div class="footer__standard d-flex">
    <div class="standard__total fs-t-14 d-flex">
      Tổng: &nbsp;
      <h1 class="title fs-t-14">40 học sinh</h1>
    </div>
    <div class="standard__btns">
      <btn-style-two nameBtn="Quay lại" class="btn__back" />
      <btn-style-one nameBtn="Lưu" class="btn__save" />
    </div>
  </div>
</template>

<script>
import BtnStyleOne from "../common/button/BtnStyleOne.vue";
import BtnStyleTwo from "../common/button/BtnStyleTwo.vue";
export default {
  components: { BtnStyleTwo, BtnStyleOne },
  name: "FooterBarStandard",
};
</script>

<style scoped>
.footer__standard {
  position: absolute;
  justify-content: space-between;
  left: 0;
  right: 0;
  bottom: 0;
  padding-top: 12px;
  padding-bottom: 12px;
  height: var(--height-pagination);
  /* background-color: aqua; */
  box-shadow: 0 0 5px #b1b1b1;
}

.standard__total {
  margin-left: 16px;
}

.standard__btns .btn__save {
  margin-left: 8px;
  margin-right: 16px;
}
</style>