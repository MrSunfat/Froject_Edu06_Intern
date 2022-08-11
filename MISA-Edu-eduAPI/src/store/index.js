import { createStore } from 'vuex';

import teachersModule from './modules/teachers';
import idxPageModule from './modules/idxPage';
import loadingModule from './modules/loading';
import newTeacherModule from './modules/newTeacher';
import searchTeacherModule from './modules/searchTeacher';

const storeData = {
    modules: {
        teachersModule,
        idxPageModule,
        loadingModule,
        newTeacherModule,
        searchTeacherModule,
    },
};

const store = createStore(storeData);
export default store;
