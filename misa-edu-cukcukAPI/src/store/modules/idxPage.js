const idxPageModule = {
    state: {
        idxPage: 1,
    },
    getters: {
        idxPage: (state) => state.idxPage,
    },
    actions: {},
    mutations: {
        /**
         * Xét giá trị của idxPage
         * @param {*} state
         * @param {*} idxPage
         * Author: Tran Danh (20/7/2022)
         */
        SET_IDXPAGE(state, idxPage) {
            state.idxPage = idxPage;
        },
        ICREMENT_IDXPAGE(state, totalPage) {
            if (state.idxPage < totalPage) {
                state.idxPage += 1;
            }
        },
        DECREMENT_IDXPAGE(state) {
            if (state.idxPage > 2) {
                state.idxPage -= 1;
            }
        },
    },
};

export default idxPageModule;
