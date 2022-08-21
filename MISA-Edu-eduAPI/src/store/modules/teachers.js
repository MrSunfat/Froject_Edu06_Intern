import axios from 'axios';
import urlTeachers from '@/scripts/constants/urlTeachers';
import constants from '@/scripts/constants/constants';

const teachersModule = {
    state: {
        teachers: [],
        totalTeachers: 0,
        totalPage: 0,
        teacherCurrent: {
            id: '',
            name: '',
        },
        listTeacherIdDelete: [],
        isTeacherCodeDuplicate: false,
    },
    getters: {
        teachers: (state) => state.teachers,
        isCheckAllTeachers: (state) => {
            let totalChecked = 0;
            state.teachers.forEach((teacher) => {
                if (teacher.checked) {
                    totalChecked += 1;
                }
            });
            return totalChecked === state.teachers.length;
        },
        totalTeachers: (state) => state.totalTeachers,
        totalPage: (state) => state.totalPage,
        teacherIdCurrent: (state) => state.teacherCurrent.id,
        teacherNameCurrent: (state) => state.teacherCurrent.name,
        listTeacherIdDelete: (state) => state.listTeacherIdDelete,
        isTeacherCodeDuplicate: (state) => state.isTeacherCodeDuplicate,
    },
    actions: {
        /**
         * Gọi API -> đổ dữ liệu vào teachers
         * - Xét totalTeachers, totalPage
         * @param {*} param0
         * @param {*} idxPage
         * Author: Tran Danh (20/7/2022)
         */
        async getTeachers({ commit }, idxPage) {
            try {
                const response = await axios.get(
                    `${urlTeachers}/filter?pageSize=${constants.pageSize}&pageNumber=${idxPage}`
                );

                response.data.Data = response.data.Data.map((db) => {
                    return {
                        ...db,
                        training: true,
                        jobStatus: true,
                        checked: false,
                    };
                });

                commit('setTeacher', response.data.Data);
                commit('setTotalPage', response.data.TotalPage);
                commit('setTotalTeachers', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Lọc dữ liệu cần tìm bằng từ khóa
         * Author: Tran Danh (26/7/2022)
         */
        async filterTeacher({ commit }, searchText) {
            try {
                const response = await axios.get(
                    `${urlTeachers}/filter?pageSize=${constants.pageSize}&pageNumber=${searchText.pageNumber}&search=${searchText.content}`
                );

                commit('setTeacher', response.data.Data);
                commit('setTotalPage', response.data.TotalPage);
                commit('setTotalTeachers', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Xóa thông tin teacher = teacherId
         * @param {*} param0
         * @param {*} teacherId
         * Author: Tran Danh (21/7/2022)
         */
        async deleteTeacher({ commit }, teacherId) {
            try {
                await axios.delete(`${urlTeachers}/${teacherId}`);
                commit('deleteTeacher', teacherId);

                const response = await axios.get(
                    `${urlTeachers}/filter?pageSize=${
                        constants.pageSize
                    }&pageNumber=${1}`
                );

                response.data.Data = response.data.Data.map((db) => {
                    return {
                        ...db,
                        training: true,
                        jobStatus: true,
                        checked: false,
                    };
                });

                commit('setTeacher', response.data.Data);
                commit('setTotalPage', response.data.TotalPage);
                commit('setTotalTeachers', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Xóa thông tin teacher = teacherId
         * @param {*} param0
         * @param {*} listTeacherId
         * Author: Tran Danh (21/7/2022)
         */
        async deleteListTeacher({ commit }, listTeacherId) {
            try {
                for (let teacherId of listTeacherId) {
                    await axios.delete(`${urlTeachers}/${teacherId}`);
                    commit('deleteTeacher', teacherId);
                }

                const response = await axios.get(
                    `${urlTeachers}/filter?pageSize=${
                        constants.pageSize
                    }&pageNumber=${1}`
                );

                response.data.Data = response.data.Data.map((db) => {
                    return {
                        ...db,
                        training: true,
                        jobStatus: true,
                        checked: false,
                    };
                });

                commit('setTeacher', response.data.Data);
                commit('setTotalPage', response.data.TotalPage);
                commit('setTotalTeachers', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * thêm teacher
         * @param {*} param0
         * @param {*} newTeacher
         * Author: Tran Danh (22/7/2022)
         */
        async addNewTeacher({ commit }, newTeacher) {
            return axios
                .post(urlTeachers, newTeacher, {
                    headers: {
                        'Content-Type': 'application/json',
                    },
                })
                .then((response) => {
                    // Nếu thêm giáo viên thành công
                    if (response.data?.statusCode === 200) {
                        commit('addTeacher', newTeacher);
                        commit('setIsTeacherCodeDuplicate', false);
                    }
                    // Nếu thêm giáo viên thất bại do trùng mã giáo viên
                    if (
                        response.data?.statusCode === 400 &&
                        response.data?.devMsg.includes('trùng')
                    ) {
                        commit('setIsTeacherCodeDuplicate', true);
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        /**
         * Sửa thông tin của teacher
         * @param {*} param0
         * @param {*} teacherInfo
         */
        async editTeacherInfo({ commit }, teacherInfo) {
            try {
                await axios.put(
                    `${urlTeachers}/${teacherInfo['id']}`,
                    teacherInfo,
                    {
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8',
                        },
                    }
                );
                commit('editTeacherMutation', teacherInfo);

                const response = await axios.get(
                    `${urlTeachers}/filter?pageSize=${
                        constants.pageSize
                    }&pageNumber=${1}`
                );

                response.data.Data = response.data.Data.map((db) => {
                    return {
                        ...db,
                        training: true,
                        jobStatus: true,
                        checked: false,
                    };
                });

                commit('setTeacher', response.data.Data);
                commit('setTotalPage', response.data.TotalPage);
                commit('setTotalTeachers', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
    },
    mutations: {
        /**
         *  Xét giá trị cho teachers
         * @param {*} state
         * @param {*} teachers
         * Author: Tran Danh (20/7/2022)
         */
        setTeacher(state, teachers) {
            state.teachers = teachers;
        },
        /**
         * Thêm teacher mới
         * @param {*} state
         * @param {*} newTeacher
         * Author: Tran Danh (20/7/2022)
         */
        addTeacher(state, newTeacher) {
            state.teachers.unshift(newTeacher);
        },
        /**
         * Xóa teacher qua teacherId
         * @param {*} state
         * @param {*} teacherId
         * Author: Tran Danh (20/7/2022)
         */
        deleteTeacher(state, teacherId) {
            state.teachers = state.teachers.filter(
                (teacher) => teacher.EmployeeId !== teacherId
            );
        },
        /**
         * Sửa teacher qua teacherId và cập nhật thông tin mới
         * @param {*} state
         * @param {*} teacherInfo
         * Author: Tran Danh (25/7/2022)
         */
        editTeacherMutation(state, teacherInfo) {
            const posTeacher = state.teachers.findIndex(
                (teacher) => teacher.EmployeeId === teacherInfo.id
            );

            if (posTeacher !== -1) {
                state.teachers[posTeacher] = { ...teacherInfo };
            }
        },
        /**
         * Xử lý khi nhấn checkall
         * -> check toàn bộ row | bỏ check toàn bộ row
         * @param {*} param0
         * @param {*} checkAll
         * Author: Tran Danh (20/7/2022)
         */
        checkAllTeachers(state, checkAll) {
            state.teachers = state.teachers.map((db) => {
                return {
                    ...db,
                    checked: checkAll,
                };
            });
        },
        /**
         * Checkbox từng hàng -> toggle checked của giáo viên đó
         * @param {*} state
         * @param {*} teacherId
         * Author: Tran Danh (21/7/2022)
         */
        checkTeacher(state, teacherId) {
            state.teachers = state.teachers.map((teacher) => {
                if (teacher.TeacherID === teacherId) {
                    return {
                        ...teacher,
                        checked: !teacher.checked,
                    };
                }
                return {
                    ...teacher,
                };
            });
        },
        /**
         * Xét giá trị totalTeachers
         * @param {*} state
         * @param {*} totalTeachers
         * Author: Tran Danh (20/7/2022)
         */
        setTotalTeachers(state, totalTeachers) {
            state.totalTeachers = totalTeachers;
        },
        /**
         * Xét giá trị totalPage
         * @param {*} state
         * @param {*} totalPage
         * Author: Tran Danh (20/7/2022)
         */
        setTotalPage(state, totalPage) {
            state.totalPage = totalPage;
        },
        /**
         * Xét giá trị cho teacherIdCurrent
         * @param {*} state
         * @param {*} teacherIdCurrent
         * Author: Tran Danh (21/7/2022)
         */
        setTeacherCurrent(state, teacherCurrent) {
            state.teacherCurrent = { ...teacherCurrent };
        },
        /**
         * Thêm giá trị vào listTeacherIdDelete
         * Tran Danh (26/7/2022)
         */
        addListTeacherIdDelete(state, teacherId) {
            state.listTeacherIdDelete.push(teacherId);
        },
        /**
         * Bỏ giá trị qua teacherId khỏi listTeacherIdDelete
         * Tran Danh (26/7/2022)
         */
        removeListTeacherIdDelete(state, teacherId) {
            state.listTeacherIdDelete = state.listTeacherIdDelete.filter(
                (id) => id !== teacherId
            );
        },
        /**
         * Xét giá trị cho listTeacherIdDelete
         * Tran Danh (26/7/2022)
         */
        setListTeacherIdDelete(state, listTeacherId) {
            state.listTeacherIdDelete = [...listTeacherId];
        },
        /**
         * Xét giá trị cho isTeacherCodeDuplicate
         * Tran Danh (17/8/2022)
         */
        setIsTeacherCodeDuplicate(state, isTeacherCodeDuplicate) {
            state.isTeacherCodeDuplicate = isTeacherCodeDuplicate;
        },
    },
};

export default teachersModule;
