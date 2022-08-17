const searchTeacherModule = {
    state: {
        searchText: '',
    },
    getters: {
        searchText: (state) => state.searchText,
    },
    actions: {

    },
    mutations: {
        /**
         * Xết 
         */
        setSearchText(state, value) {
            state.searchText = value;
        }
    },
};

export default searchTeacherModule;