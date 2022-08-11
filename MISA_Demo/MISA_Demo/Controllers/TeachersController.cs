using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA_Demo.Model;

namespace MISA_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        /// 200 - Danh sách khách hàng
        /// 204 - Không có dữ liệu
        /// CreateBy: TNDanh (28/7/2022)
        [HttpGet()]
        public IActionResult getTeacher()
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand = "SELECT * FROM Teacher";
                // 2.2 Thực hiện lấy dữ liệu
                var teachers = sqlConnection.Query<object>(sql: sqlCommand);
                // trả kết quả cho Client:
                return Ok(teachers);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Lấy thông tin giáo viên qua teacherID
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)
        [HttpGet("{teacherID}")]
        public IActionResult getTeacherById(Guid teacherID)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand = "SELECT * FROM Teacher t WHERE t.TeacherID = @teacherID";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherID", teacherID);
                // 2.2 Thực hiện lấy dữ liệu
                var teacher = sqlConnection.QueryFirstOrDefault<object>(sql: sqlCommand, param: parameters);
                // trả kết quả cho Client:
                return Ok(teacher);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Thêm thông tin giáo viên mới
        /// </summary>
        /// <param name="newTeacher"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)

        [HttpPost()]
        public IActionResult addNewTeacher([FromBody] Teacher newTeacher)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand =
                    "INSERT INTO Teacher (TeacherID, TeacherCode, FullName, Email, PhoneNumber, IsProfessionalQualifications, IsWorking)" +
                    " VALUES (@teacherID, @teacherCode, @fullName, @email, @phoneNumber, @isProfessionalQualifications, @isWorking); ";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherID", Guid.NewGuid().ToString());
                parameters.Add("@teacherCode", newTeacher.TeacherCode);
                parameters.Add("@fullName", newTeacher.FullName);
                parameters.Add("@email", newTeacher.Email);
                parameters.Add("@phoneNumber", newTeacher.PhoneNumber);
                parameters.Add("@isProfessionalQualifications", newTeacher.IsProfessionalQualifications);
                parameters.Add("@isWorking", newTeacher.IsWorking);
                // 2.2 Thực hiện thêm dữ liệu
                var teacher = sqlConnection.Query<Teacher>(sql: sqlCommand, param: parameters);
                // trả kết quả cho Client:
                return Ok(newTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            /**
             * {
                "TeacherCode": "SHCB-7957",
                "FullName": "Nguyễn Văn Công",
                "Email": "nguyencong2k1@gmail.com",
                "PhoneNumber": "092190307564",
                "IsProfessionalQualifications": true,
                "IsWorking": true
                }   
             */
        }

        /// <summary>
        /// Sửa thông tin giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)

        [HttpPut("{teacherID}")]
        public IActionResult editTeacher(Guid teacherId, [FromBody] Teacher teacherItem)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand =
                    "UPDATE Teacher t" + " SET t.teacherCode = @teacherCode " + "WHERE t.TeacherID = @teacherID;";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherID", teacherId);
                parameters.Add("@teacherCode", teacherItem.TeacherCode);
                // 2.2 Thực hiện cập nhật dữ liệu
                var teacher = sqlConnection.Query<Teacher>(sqlCommand, parameters);
                // trả kết quả cho Client:
                return Ok(teacherItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            /**
             * {
                "TeacherCode": "SHCB-7957",
                "FullName": "Nguyễn Văn Công",
                "Email": "nguyencong2k1@gmail.com",
                "PhoneNumber": "092190307564",
                "IsProfessionalQualifications": true,
                "IsWorking": true
                }   
             */
        }

        /// <summary>
        /// Xóa giáo viên qua teacherID
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)
        [HttpDelete("{teacherID}")]
        public IActionResult deleteTeacherById(Guid teacherID)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand = "DELETE FROM Teacher WHERE TeacherID = @teacherID";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherID", teacherID);
                // 2.2 Thực hiện lấy dữ liệu
                sqlConnection.Query<object>(sql: sqlCommand, param: parameters);
                // trả kết quả cho Client:
                //return Ok(teacher);
                return StatusCode(204, "Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }


        /// <summary>
        /// Xóa nhiều teacher qua danh sách các teacherID
        /// </summary>
        /// <param name="listTeacherID"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022
        [HttpDelete()]
        public IActionResult deleteListTeacher([FromBody]string[] listTeacherID)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Liệt kê các teacherID cần xóa:
                var listID = "";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                for (int i = 0; i < listTeacherID.Length; i++)
                {
                    string param = $"@param{i}";
                    if (i < listTeacherID.Length - 1)
                    {
                        listID += param + ",";
                    } else
                    {
                        listID += param;
                    }

                    parameters.Add(param, listTeacherID[i]);
                }
                // 2.1 Câu lệnh truy vấn xóa dữ liệu:
                var sqlCommand = $"DELETE FROM Teacher WHERE TeacherID IN ({listID})";
                // 2.2 Thực hiện xóa dữ liệu
                var teacher = sqlConnection.Query<Teacher>(sql: sqlCommand, param: parameters);
                // trả kết quả cho Client:
                return Ok(listTeacherID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

            /*
             * Ví dụ:
                ListTeacherID: 27ce856d-0e5a-11ed-82ee-0259e1bc84a2, df7657dd-0e5d-11ed-82ee-0259e1bc84a2
             */
        }
    }
}
