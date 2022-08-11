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

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
                // console.log(response.data);
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
                    `${urlTeachers}/filter?pageSize=${constants.pageSize}&pageNumber=${searchText.pageNumber}&employeeFilter=${searchText.content}`
                );

                console.log('Data: ', response.data);

                // response.data.Data = response.data.Data.map((db) => {
                //     return {
                //         ...db,
                //         training: true,
                //         jobStatus: true,
                //         checked: false,
                //     };
                // });

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
                // console.log(response.data);
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
                console.log('Xóa teacher: ', teacherId);
                commit('DELETE_TEACHER', teacherId);

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

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
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
                    console.log('Xóa teacher: ', teacherId);
                    commit('DELETE_TEACHER', teacherId);
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

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
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
            try {
                // await axios.post(`${urlTeachers}${newTeacher}`);
                await axios.post(urlTeachers, newTeacher, {
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
                commit('ADD_TEACHER', newTeacher);

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

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Sửa thông tin của teacher
         * @param {*} param0
         * @param {*} teacherInfo
         */
        async editTeacherInfo({ commit }, teacherInfo) {
            try {
                // await axios.post(`${urlTeachers}${newTeacher}`);
                console.log(teacherInfo);
                await axios.put(
                    `${urlTeachers}/${teacherInfo.id}`,
                    teacherInfo,
                    {
                        headers: {
                            'content-Type': 'application/json; charset=utf-8',
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

                commit('SET_TEACHER', response.data.Data);
                commit('SET_TOTALPAGE', response.data.TotalPage);
                commit('SET_TOTALTEACHERS', response.data.TotalRecord);
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
        SET_TEACHER(state, teachers) {
            state.teachers = teachers;
        },
        /**
         * Thêm teacher mới
         * @param {*} state
         * @param {*} newTeacher
         * Author: Tran Danh (20/7/2022)
         */
        ADD_TEACHER(state, newTeacher) {
            state.teachers.unshift(newTeacher);
        },
        /**
         * Xóa teacher qua teacherId
         * @param {*} state
         * @param {*} teacherId
         * Author: Tran Danh (20/7/2022)
         */
        DELETE_TEACHER(state, teacherId) {
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
        CHECKALL_TEACHERS(state, checkAll) {
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
        CHECK_TEACHER(state, teacherId) {
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
        SET_TOTALTEACHERS(state, totalTeachers) {
            state.totalTeachers = totalTeachers;
        },
        /**
         * Xét giá trị totalPage
         * @param {*} state
         * @param {*} totalPage
         * Author: Tran Danh (20/7/2022)
         */
        SET_TOTALPAGE(state, totalPage) {
            state.totalPage = totalPage;
        },
        /**
         * Xét giá trị cho teacherIdCurrent
         * @param {*} state
         * @param {*} teacherIdCurrent
         * Author: Tran Danh (21/7/2022)
         */
        SET_TEACHER_CURRENT(state, teacherCurrent) {
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
    },
};

export default teachersModule;
