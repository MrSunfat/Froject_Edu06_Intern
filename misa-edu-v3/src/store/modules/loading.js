const loadingModule = {
    state: {
        loading: {
            isShowLoading: false,
        },
    },
    getters: {
        isShowLoading: (state) => state.loading.isShowLoading,
    },
    actions: {},
    mutations: {
        TOGGLE_SHOW_LOADING(state) {
            state.loading.isShowLoading = !state.loading.isShowLoading;
        },
        SHOW_LOADING(state) {
            state.loading.isShowLoading = true;
        },
        HIDE_LOADING(state) {
            state.loading.isShowLoading = false;
        }
    },
};

export default loadingModule;