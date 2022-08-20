<template>
  <div class="abstract-page">
    Tính năng đang phát triển
  </div>
</template>

<script>
export default {
  name: "AbstractPage",
  components: {
  },
  data() {
    return {
      listDepartment: [
        {
          Name: "Tổ Toán Tin",
          ID: 1,
        },
        {
          Name: "Tổ Hóa Lý",
          ID: 2,
        },
        {
          Name: "Tổ Văn học",
          ID: 3,
        },
        {
          Name: "Tổ Sinh Địa",
          ID: 4,
        },
        {
          Name: "Tổ Sử GDCD",
          ID: 5,
        },
        {
          Name: "Tổ Thể chất - Mỹ thuật",
          ID: 6,
        },
      ],
      teacher: {
        Department: "",
      },
      onMultiTagPreparingDepartment: (args) => {
        let itemSize = args.selectedItems.length;
        let totalCount = this.listDepartment.length;
        if (itemSize < totalCount) {
          args.cancel = true;
        } else {
          args.text = "Tất cả";
        }
      },
    };
  },
  methods: {
    addStyle() {
      this.$refs["ms-selectbox"].$el.style = "top: 100px !important";
    },
  },
  mounted() {
    
    //this.addStyle();
  },
};
</script>

<style scoped>
.dx-scrollview-content {
  top: 8px !important;
}

.dx-texteditor.dx-state-hover {
  border-color: var(--active-border-input) !important;
}

.dx-texteditor.dx-state-focused.dx-editer-outlined {
  border-color: var(--active-border-input) !important;
}

.dx-dropdowneditor.dx-dropdowneditor-active .dx-dropdowneditor-icon {
  background-color: transparent !important;
}

.dx-state-focused {
  border-color: #0fa655 !important;
}
.dx-state-active {
  border-color: #0fa655 !important;
}

.dx-tag-content {
  border-radius: 4px;
}

.dx-tag-remove-button {
  position: absolute;
  top: -1px;
  right: 2px;
  width: 25px;
  height: 100%;
}

.dx-checkbox-checked .dx-checkbox-icon {
  color: #fff;
}

.dx-checkbox-checked .dx-checkbox-icon:active {
  background-color: #0fa655;
}

.dx-list.dx-list-select-decorator-enabled
  .dx-list-item.dx-state-focused
  .dx-checkbox-icon,
.dx-list.dx-list-select-decorator-enabled
  .dx-list-item.dx-state-focused
  .dx-radiobutton-icon::before,
.dx-list.dx-list-select-decorator-enabled
  .dx-list-select-all.dx-state-focused
  .dx-checkbox-icon,
.dx-list.dx-list-select-decorator-enabled
  .dx-list-select-all.dx-state-focused
  .dx-radiobutton-icon::before {
  border-color: #0fa655;
}

.dx-checkbox-checked .dx-checkbox-icon,
.dx-list-item-selected .dx-checkbox-icon {
  background-color: #0fa655;
}

.dx-state-hover .dx-checkbox-icon {
  border: 1px solid #0fa655 !important;
}

.dx-checkbox-indeterminate .dx-checkbox-icon::before {
  background-color: #fff;
}

.dx-texteditor.dx-state-hover {
  border-color: #bbb !important;
}

.dx-texteditor.dx-state-focused.dx-editor-outlined {
  border-color: #0fa655;
}

.dx-checkbox-checked .dx-checkbox-icon::before {
  top: calc(50% - 1px) !important;
  color: #fff !important;
}

.dx-checkbox-icon {
  width: 20px !important;
  height: 20px !important;
  border-radius: 5px !important;
}

.dx-tag-container.dx-texteditor-input-container {
  padding: 0 4px 0 0;
  outline: 0;
}

.abstract-page {
  width: 100%;
  height: 100%;
}

.dx-dropdownlist-popup-wrapper.dx-popup-wrapper .dx-overlay-content {
  top: 200px !important;
}
</style>