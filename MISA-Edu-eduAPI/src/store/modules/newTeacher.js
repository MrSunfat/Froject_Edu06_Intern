const newTeacherModule = {
    state: {
        teacher: {
            TeacherCode: '',
            FullName: '',
            PhoneNumber: '',
            Email: '',
        },
    },
    getters: {
        teacher: (state) => state.teacher,
        TeacherCode: (state) => state.teacher.TeacherCode,
        FullName: (state) => state.teacher.FullName,
        PhoneNumber: (state) => state.teacher.PhoneNumber,
        Email: (state) => state.teacher.Email,
    },
    actions: {},
    mutations: {
        /**
         * Xét giá trị cho teacherCode
         * @param {*} state
         * @param {*} teacherCode
         * Author: Tran Danh (21/7/2022)
         */
        setTeacherCode(state, TeacherCode) {
            state.teacher.TeacherCode = TeacherCode;
        },
        /**
         * Xét giá trị cho fullName
         * @param {*} state
         * @param {*} fullName
         * Author: Tran Danh (21/7/2022)
         */
        SET_FULLNAME(state, FullName) {
            state.teacher.FullName = FullName;
        },
        /**
         * xét giá trị cho PhoneNumber
         */
        setPhoneNumber(state, PhoneNumber) {
            state.teacher.PhoneNumber = PhoneNumber;
        },
        /**
         * Xét giá trị cho Email
         */
        setEmail(state, Email) {
            state.teacher.Email = Email;
        },
        /**
         * Xét giá trị của newTeacher
         * Author: Tran Danh (22/7/2022)
         */
        SET_NEWTEACHER(state, teacher) {
            state.teacher = teacher;
        },
        /**
         * Xét empty cho teacher
         */
        setEmptyTeacher(state) {
            state.teacher = {
                TeacherCode: '',
                FullName: '',
                PhoneNumber: '',
                Email: '',
            };
        },
    },
};

export default newTeacherModule;
