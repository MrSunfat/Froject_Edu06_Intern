using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;

namespace MISA_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// lấy thông tin các phân ban
        /// </summary>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)
        [HttpGet]
        public IActionResult getDapartments()
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand = "SELECT * FROM Department";
                // 2.2 Thực hiện lấy dữ liệu
                var teacher = sqlConnection.Query<object>(sql: sqlCommand);
                // trả kết quả cho Client:
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy thông tin phòng ban bằng departmentID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        /// CreateBy: TNDanh (28/7/2022)
        [HttpGet("{departmentID}")]
        public IActionResult getDepartmentById(Guid departmentID)
        {
            try
            {
                // 1. Khởi tạo kết nối với database:
                var connectionString = "Host=3.0.89.182; Port=3306; Database = MISA.WEB06.TNDANH; User Id = dev; Password = 12345678";
                // 1.1 Khởi tạo kết nối với MariaDb:
                var sqlConnection = new MySqlConnection(connectionString);
                // 2. lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu:
                var sqlCommand = "SELECT * FROM Department d WHERE d.DepartmentID = @departmentID";
                // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@departmentID", departmentID);
                // 2.2 Thực hiện lấy dữ liệu
                var teacher = sqlConnection.Query<object>(sql: sqlCommand, param: parameters);
                // trả kết quả cho Client:
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
