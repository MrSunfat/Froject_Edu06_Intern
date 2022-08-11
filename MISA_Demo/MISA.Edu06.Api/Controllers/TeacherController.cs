using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;
using MISA.Edu06.Core.Resources;
using MISA.Edu06.Core.Services;
using MISA.Edu06.Infrastructure.Repository;

namespace MISA.Edu06.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherRepository teacherRepository, ITeacherService teacherService)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
        }

        /// <summary>
        /// Lấy ra thông tin tất cả giáo viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Thực hiện lấy dữ liệu
                var teachers = _teacherRepository.GetAll();
                var res = new
                {
                    TotalRecord = teachers.Count(),
                    Data = teachers
                };
                return Ok(teachers);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// Lấy thông tin của giáo viên qua teacherID
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        [HttpGet("{teacherID}")]
        public IActionResult GetTeacherByID(Guid teacherID)
        {
            try
            {
                // Thực hiện lấy dữ liệu qua teacherID
                var teacher = _teacherRepository.GetTeacherByID(teacherID);
                return Ok(teacher);

            } catch(Exception ex)
            {
                var error = new
                {
                    ex = ex.Message,
                    message = "Không tìm thấy giáo viên",
                    devMes = "Không tìm thấy teacherID",
                };
                return StatusCode(400, error);
            }

        }

        /// <summary>
        /// Tạo ra một mã giáo viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: TNDanh (8/8/2022)
        [HttpGet("NewTeacherCode")]
        public string NewTeacherCode()
        {
            var res = _teacherRepository.NewTeacherCode();
            return res;
        }

        /// <summary>
        /// Tìm kiếm bằng mã giáo viên hoặc tên giáo viên và phân trang
        /// </summary>
        /// <param name="teacherCode"></param>
        /// <param name="teacherName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (10/8/2022)
        [HttpGet("Filter")]
        public IActionResult FilterTeacher([FromQuery]string? search, [FromQuery]int pageSize = 20, [FromQuery]int pageNumber = 1)
        {
            var res = _teacherRepository.FilterTeacher(search, pageSize, pageNumber);
            return Ok(res);
        }

        /// <summary>
        /// Thêm giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (3/8/2022)
        [HttpPost()]
        public IActionResult AddNewTeacher(Teacher teacher)
        {   
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _teacherService.InsertService(teacher);
                // Thêm một giáo viên mới
                var res = _teacherRepository.AddNewTeacher(teacher);

                var response = new
                {
                    devMsg = "Success Post",
                    userMsg = "Success Post"
                };

                return Ok(response);

            } 
            catch(EduValidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = teacher
                };
                return BadRequest(response);
            } 

        }

        /// <summary>
        /// Sửa thông tin giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (4/8/2022)
        [HttpPut("{teacherID}")]
        public IActionResult EditTeacher(Teacher teacher, Guid teacherID)
        {
            var res = _teacherRepository.UpdateTeacherByID(teacher, teacherID);
            return Ok(res);

        }

        /// <summary>
        /// Xóa teacher qua teacherID
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (3/8/2022)
        [HttpDelete("{teacherID}")]
        public IActionResult DeleteTeacherByID(Guid teacherID)
        {
            try
            {
                var teacher = _teacherRepository.DeleteTeacherByID(teacherID);
                return Ok(teacher);

            } catch (Exception ex)
            {
                var error = new
                {
                    ex = ex.Message,
                    message = "Không tìm thấy giáo viên",
                    devMes = "Không tìm thấy teacherID",
                };
                return StatusCode(400, error);
            }
        }

        /// <summary>
        ///  Xóa nhiều giáo viên qua teacherID từ body
        /// </summary>
        /// <param name="listTeacher"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteListTeacher([FromBody] Guid[] listTeacher)
        {
            try
            {
                var res = _teacherRepository.DeleteListTeacher(listTeacher);
                return StatusCode(StatusCodes.Status200OK, "Xóa các giáo viên thành công");
            } catch(Exception ex)
            {
                var error = new
                {
                    ex = ex.Message,
                    message = "Không tìm thấy giáo viên",
                    devMes = "Không tìm thấy teacherID",
                };
                return BadRequest(error);
            }
        }
    }
}
