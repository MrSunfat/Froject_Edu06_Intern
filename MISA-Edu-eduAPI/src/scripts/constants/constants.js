const titleLoading = 'Đang tải';
const pageSize = 20;
const propertiesTeacher = {
    code: 'Số hiệu cán bộ',
    fullName: 'Họ và tên',
    phoneNumber: 'Số điện thoại',
    email: 'Email',
    subjectGroup: 'Tổ hợp môn',
    subject: 'QL theo môn',
    department: 'QL kho, phòng',
};
const warningContent = {
    codeDuplicate: "Số hiệu cán bộ đã tồn tại trong hệ thống vui lòng kiểm tra lại."
}
const regexpFormat = {
    phone: /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/,
    email: /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/,
};
const titleFile = "Danh sách giáo viên.xlsx";

export default Object.freeze({
    titleLoading,
    pageSize,
    propertiesTeacher,
    regexpFormat,
    titleFile,
    warningContent,
});
