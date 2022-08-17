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
        /**
         * 
         * @param {*} state 
         */
        toggleShowLOADING(state) {
            state.loading.isShowLoading = !state.loading.isShowLoading;
        },
        showLoading(state) {
            state.loading.isShowLoading = true;
        },
        hideLoading(state) {
            state.loading.isShowLoading = false;
        }
    },
};

export default loadingModule;