# Froject_Edu06_Intern
`1. Front-end`: "Vue"
- Làm giao diện quản lý thông tin giáo viên
  + Có bảng hiển thị thông tin giáo viên
  + Thêm, sửa, xóa một/nhiều các giáo viên
  + Có validate các trường trong form thêm / sửa
  + Chức năng phân trang
- Vue Router: chuyển các trang (page)
- Vuex: quản lý dữ liệu cần thiết tập trung
  + Thông tin giáo viên
  + Thông tin môn học, tổ hợp môn, phòng
`2. Back-end`: "ASP.NET Core theo clean architecture"
* Chia 3 lớp
- Infrastructure: Thao tác với DB
- Core:
  + Chứa các interfaces: tạo ra để định nghĩa các phương thức phải có
  + Resources: lưu các thông báo
  + Services: kiểm tra các trường thông tin của đối tượng (giáo viên, môn,...)
- API: đẩy ra các truy vấn dưới -> các api json
`3. DB`: "MariaDB - dbForge"
- Tạo 4 bảng chính: giáo viên, môn, tổ hợp môn, phòng thiết bị
- 2 bảng liên kết: giáo viên-môn, giáo viên-phòng thiết bị
