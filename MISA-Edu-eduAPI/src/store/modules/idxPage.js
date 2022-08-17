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
        setIdxPage(state, idxPage) {
            state.idxPage = idxPage;
        },
        incrementIdxPage(state, totalPage) {
            if (state.idxPage < totalPage) {
                state.idxPage += 1;
            }
        },
        decrementIdxPage(state) {
            if (state.idxPage > 2) {
                state.idxPage -= 1;
            }
        },
    },
};

export default idxPageModule;
