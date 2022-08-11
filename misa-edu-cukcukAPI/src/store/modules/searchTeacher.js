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
        setSearchText(state, value) {
            state.searchText = value;
        }
    },
};

export default searchTeacherModule;