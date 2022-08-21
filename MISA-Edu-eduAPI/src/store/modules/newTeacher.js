const newTeacherModule = {
    state: {
        teacher: {
            TeacherCode: '',
            FullName: '',
            PhoneNumber: '',
            Email: '',
            AvatarSrc: '',
            Files: '',
            ImgByte: '',
            DepartmentID: 1,
            ListSubject: [],
            ListRoom: [],
            IsProfessionalQualifications: false,
            IsWorking: false,
            DayOff: '',
        },
    },
    getters: {
        teacher: (state) => state.teacher,
        TeacherCode: (state) => state.teacher.TeacherCode,
        FullName: (state) => state.teacher.FullName,
        PhoneNumber: (state) => state.teacher.PhoneNumber,
        Email: (state) => state.teacher.Email,
        AvatarSrc: (state) => state.teacher.AvatarSrc,
        IsWorking: (state) => state.teacher.IsWorking,
        IsProfessionalQualifications: (state) =>
            state.teacher.IsProfessionalQualifications,
        DepartmentID: (state) => state.teacher.DepartmentID,
        ListSubject: (state) => state.teacher.ListSubject,
        ListRoom: (state) => state.teacher.ListRoom,
    },
    actions: {},
    mutations: {
        /**
         * Xét giá trị cho teacherCode
         * Author: Tran Danh (21/7/2022)
         */
        setTeacherCode(state, TeacherCode) {
            state.teacher.TeacherCode = TeacherCode;
        },
        /**
         * Xét giá trị cho fullName
         * Author: Tran Danh (21/7/2022)
         */
        setFullName(state, FullName) {
            state.teacher.FullName = FullName;
        },
        /**
         * xét giá trị cho PhoneNumber
         * Author: Tran Danh (22/7/2022)
         */
        setPhoneNumber(state, PhoneNumber) {
            state.teacher.PhoneNumber = PhoneNumber;
        },
        /**
         * Xét giá trị cho Email
         * Author: Tran Danh (22/7/2022)
         */
        setEmail(state, Email) {
            state.teacher.Email = Email;
        },
        /**
         * Xét url cho ảnh đại diện
         * Author: Tran Danh (21/8/2022)
         */
        setAvatarSrc(state, avatarSrc) {
            state.teacher.AvatarSrc = avatarSrc;
        },
        /**
         *  Xét giá trị cho đào tạo trình độ
         * Author: Tran Danh (22/7/2022)
         */
        setIsProfessionalQualifications(state, isProfessionalQualifications) {
            state.teacher.IsProfessionalQualifications =
                isProfessionalQualifications;
        },
        /**
         * Xét giá trị của tình trạng làm việc
         * Author: Tran Danh (22/7/2022)
         */
        setIsWorking(state, isWorking) {
            state.teacher.IsWorking = isWorking;
        },
        /**
         * Xét giá trị ngày tháng nghỉ việc
         * Author: Tran Danh (22/7/2022)
         */
        setDayOff(state, dayOff) {
            state.teacher.DayOff = dayOff;
        },
        /**
         * Xét giá trị id của tổ hợp môn
         * Author: Tran Danh (16/8/2022)
         */
        setDepartmentID(state, departmentId) {
            state.teacher.DepartmentID = departmentId;
        },
        /**
         * Xét giá trị các id môn cho danh sách môn
         * Author: Tran Danh (16/8/2022)
         */
        setListSubject(state, listSubject) {
            state.teacher.ListSubject = listSubject;
        },
        /**
         * Xét giá trị các id phòng cho danh sách phòng
         * Author: Tran Danh (16/8/2022)
         */
        setListRoom(state, listRoom) {
            state.teacher.ListRoom = listRoom;
        },
        /**
         * Xét giá trị của newTeacher
         * Author: Tran Danh (22/7/2022)
         */
        setNewTeacher(state, teacher) {
            state.teacher = teacher;
        },
        /**
         * Xét empty cho teacher
         * Author: Tran Danh (22/7/2022)
         */
        setEmptyTeacher(state) {
            state.teacher = {
                TeacherCode: '',
                FullName: '',
                PhoneNumber: '',
                Email: '',
                DepartmentID: 1,
                ListSubject: [],
                ListRoom: [],
                IsProfessionalQualifications: false,
                IsWorking: false,
                DayOff: '',
            };
        },
    },
};

export default newTeacherModule;
