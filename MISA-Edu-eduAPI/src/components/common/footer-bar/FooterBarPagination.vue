<template>
  <div class="footer__pagination d-flex">
    <div class="pagination__direction d-flex">
      <div class="first-page-icon d-flex c-p" @click="handleFirstPage">
        <img :src="firstPageIcon" alt="" />
      </div>
      <div class="prev-page-icon d-flex c-p" @click="handlePrevPage">
        <img :src="prevPageIcon" alt="" />
      </div>
      <input
        type="text"
        class="index-page"
        v-model="indexPage"
        @change="handleFocusEvent"
      />
      <div class="next-page-icon d-flex c-p" @click="handleNextPage">
        <img :src="nextPageIcon" alt="" />
      </div>
      <div class="last-page-icon d-flex c-p" @click="handleLastPage">
        <img :src="lastPageIcon" alt="" />
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
import LoadingComp from "@/components/common/loading/LoadingComp.vue";
import firstPageIcon from "@/assets/Icons/ic_MoveToFirst.png";
import prevPageIcon from "@/assets/Icons/ic_Back.png";
import nextPageIcon from "@/assets/Icons/ic_Next.png";
import lastPageIcon from "@/assets/Icons/ic_MoveToLast.png";

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
      firstPageIcon,
      prevPageIcon,
      nextPageIcon,
      lastPageIcon,
    };
  },
  methods: {
    /**
     * Xử lý khi nhập vào ô -> đi đến trang mong muốn
     */
    handleFocusEvent() {
      this.indexPage = parseInt(this.indexPage);
      this.dumpDataToTable(this.indexPage);
      this.setIdxPage(this.indexPage);
    },
    /**
     * Xử lý khi nhấn trang trước đó
     * Author: Tran Danh (21/7/2022)
     */
    handlePrevPage() {
      if (this.indexPage > 1) {
        this.indexPage = parseInt(this.indexPage) - 1;
        this.setIdxPage(this.indexPage);
        this.dumpDataToTable(this.indexPage);
      }
    },
    /**
     * Xử lý khi nhấn vào nút quay về trang đầu
     * Author: Tran Danh (21/7/2022)
     */
    handleFirstPage() {
      if (this.indexPage > 1) {
        this.indexPage = 1;
        this.setIdxPage(this.indexPage);
        this.dumpDataToTable(this.indexPage);
      }
    },
    /**
     * Xử lý khi nhấn trang tiếp theo
     * Author: Tran Danh (21/7/2022)
     */
    handleNextPage() {
      if (this.indexPage < this.totalPage) {
        this.indexPage = parseInt(this.indexPage) + 1;
        this.setIdxPage(this.indexPage);
        this.dumpDataToTable(this.indexPage);
      }
    },
    /**
     * Xử lý khi nhấn vào nút đến trang cuối
     * Author: Tran Danh (21/7/2022)
     */
    handleLastPage() {
      if (this.indexPage < this.totalPage) {
        this.indexPage = this.totalPage;
        this.setIdxPage(this.indexPage);
        this.dumpDataToTable(this.indexPage);
      }
    },
    /**
     * Load data bằng cách truyền chỉ số page
     * Author: Tran Danh (21/7/2022)
     */
    dumpDataToTable(indexPage) {
      const me = this;
      me.showLoading();
      if (indexPage >= 1 && indexPage <= this.totalPage) {
        this.setIdxPage(indexPage);
      } else if (indexPage < 1) {
        this.setIdxPage(1);
        this.indexPage = 1;
      } else if (indexPage > this.totalPage) {
        this.setIdxPage(this.totalPage);
        this.indexPage = this.totalPage;
      }

      this.$emit("setIdxWhenSearchText", this.idxPage);

      if (!this.searchText.content) {
        this.getTeachers(this.idxPage);
      } else {
        this.filterTeacher(this.searchText);
      }
    },
    ...mapMutations(["setIdxPage", "showLoading", "hideLoading"]),
    ...mapActions(["getTeachers", "filterTeacher"]),
  },
  computed: {
    ...mapGetters([
      "isShowLoading",
      "idxPage",
      "teachers",
      "totalTeachers",
      "totalPage",
    ]),
  },

  created() {
    this.indexPage = this.idxPage;
  },
  watch: {
    teachers() {
      this.hideLoading();
    },
    idxPage() {
      this.indexPage = this.idxPage;
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