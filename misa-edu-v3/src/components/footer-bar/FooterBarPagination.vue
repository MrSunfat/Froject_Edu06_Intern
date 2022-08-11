<template>
  <div class="footer__pagination d-flex">
    <div class="pagination__direction d-flex">
      <div class="first-page-icon d-flex c-p" @click="handleFirstPage">
        <img src="../../assets/Icons/ic_MoveToFirst.png" alt="" />
      </div>
      <div class="prev-page-icon d-flex c-p" @click="handlePrevPage">
        <img src="../../assets/Icons/ic_Back.png" alt="" />
      </div>
      <input
        type="text"
        class="index-page"
        v-model="indexPage"
        @change="handleFocusEvent"
      />
      <div class="next-page-icon d-flex c-p" @click="handleNextPage">
        <img src="../../assets/Icons/ic_Next.png" alt="" />
      </div>
      <div class="last-page-icon d-flex c-p" @click="handleLastPage">
        <img src="../../assets/Icons/ic_MoveToLast.png" alt="" />
      </div>
      <div class="number-page-current subtitle-two fs-t-14 d-flex">
        {{ this.idxPage }}/{{ totalPage }} trang (
        <h1 class="subtitle-one fs-t-14 result">{{ totalTeachers }}</h1>
        &nbsp;kết quả)
      </div>
    </div>
    <div class="pagination__btns">
      <slot></slot>
    </div>
    <loading-comp v-if="isShowLoading" />
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapActions } from "vuex";
import LoadingComp from "../common/loading/LoadingComp.vue";

export default {
  name: "FooterBarPagination",
  components: { LoadingComp },
  props: {
    searchText: {
      type: Object,
    },
  },
  data() {
    return {
      indexPage: 1,
    };
  },
  methods: {
    /**
     * Xử lý khi nhập vào ô -> đi đến trang mong muốn
     */
    handleFocusEvent() {
      // console.log(this.indexPage);
      this.indexPage = parseInt(this.indexPage);
      this.dumpDataToTable(this.indexPage);
    },
    /**
     * Xử lý khi nhấn trang trước đó
     * Author: Tran Danh (21/7/2022)
     */
    handlePrevPage() {
      if (this.indexPage > 1) {
        this.indexPage = parseInt(this.indexPage) - 1;
      }
      this.dumpDataToTable(this.indexPage);
    },
    /**
     * Xử lý khi nhấn vào nút quay về trang đầu
     * Author: Tran Danh (21/7/2022)
     */
    handleFirstPage() {
      this.indexPage = 1;
      this.dumpDataToTable(this.indexPage);
    },
    /**
     * Xử lý khi nhấn trang tiếp theo
     * Author: Tran Danh (21/7/2022)
     */
    handleNextPage() {
      if (this.indexPage < this.totalPage) {
        this.indexPage = parseInt(this.indexPage) + 1;
      }
      console.log();
      this.dumpDataToTable(this.indexPage);
    },
    /**
     * Xử lý khi nhấn vào nút đến trang cuối
     * Author: Tran Danh (21/7/2022)
     */
    handleLastPage() {
      this.indexPage = this.totalPage;
      this.dumpDataToTable(this.indexPage);
    },
    /**
     * Load data bằng cách truyền chỉ số page
     * Author: Tran Danh (21/7/2022)
     */
    dumpDataToTable(indexPage) {
      console.log("Vi tri: ", indexPage);
      console.log(this.indexPage, this.totalPage);
      this.SHOW_LOADING();
      if (indexPage >= 1 && indexPage <= this.totalPage) {
        this.SET_IDXPAGE(indexPage);
      } else if (indexPage < 1) {
        this.SET_IDXPAGE(1);
        this.indexPage = 1;
      } else if (indexPage > this.totalPage) {
        this.SET_IDXPAGE(this.totalPage);
        this.indexPage = this.totalPage;
      }

      this.searchText.pageNumber;

      if (!this.searchText.content) {
        this.getTeachers(this.idxPage);
      } else {
        this.filterTeacher(this.idxPage);
      }
    },
    ...mapMutations([
      "SET_IDXPAGE",
      "ICREMENT_IDXPAGE",
      "DECREMENT_IDXPAGE",
      "SHOW_LOADING",
      "HIDE_LOADING",
    ]),
    ...mapActions(["getTeachers", "filterTeacher"]),
  },
  computed: mapGetters([
    "isShowLoading",
    "idxPage",
    "teachers",
    "totalTeachers",
    "totalPage",
  ]),
  created() {
    this.indexPage = this.idxPage;
  },
  watch: {
    teachers() {
      this.HIDE_LOADING();
    },
  },
};
</script>

<style scoped>
.footer__pagination {
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
  background-color: #fff;
}

.pagination__direction {
  justify-content: space-between;
  padding-left: 16px;
}

.first-page-icon {
  margin-right: 4px;
}

.index-page {
  text-align: center;
  width: 64px;
  height: 32px;
  border-radius: 4px;
  border: 1px solid var(--second-btn-bd);
  color: var(--input-text-color);
  margin: 0 10px;
}

.index-page:focus {
  outline: unset;
  border-color: var(--active-border-input);
}

.next-page-icon {
  margin-right: 3px;
}

.last-page-icon {
  margin-right: 10px;
}

.number-page-current > .result {
  font-weight: 500;
}

.pagination__btns .btn__save {
  margin-left: 8px;
  margin-right: 16px;
}

.pagination__btns {
  padding-right: 16px;
}
</style>