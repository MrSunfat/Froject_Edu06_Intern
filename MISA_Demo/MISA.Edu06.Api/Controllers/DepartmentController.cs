using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;

namespace MISA.Edu06.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Properties
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentService departmentService) 
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Lấy tất cả tổ hợp môn
        /// </summary>
        /// <returns>Tất cả tổ hợp môn</returns>
        /// CreatedBy: TNDanh (15/8/2022)
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _departmentRepository.GetAll();
            return Ok(res);
        }

        /// <summary>
        /// Thêm tổ hợp môn mới
        /// </summary>
        /// <param name="department">Thông tin của tổ hợp môn mới</param>
        /// <returns>Số lượng tổ hợp môn thêm</returns>
        /// CreatedBy: TNDanh (15/8/2022)
        [HttpPost()]
        public IActionResult AddNewDepartment(Department department)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _departmentService.ValidateService(department);
                // Thêm một giáo viên mới
                var res = _departmentRepository.AddNewDepartment(department);

                return Ok(Resources.StatusMesg.S_Post);

            }
            catch (EduValidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = department
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Sửa thông tin tổ hợp môn
        /// </summary>
        /// <param name="department">Thông tin mới của tổ hợp môn</param>
        /// <param name="departmentID">Mã ID của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn sửa đổi</returns>
        /// CreatedBy: TNDanh (15/8/2022)
        [HttpPut("{departmentID}")]
        public IActionResult EditDepartment(Department department, int departmentID)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _departmentService.ValidateService(department);
                // Thêm một giáo viên mới
                var res = _departmentRepository.UpdateDepartmentByID(department, departmentID);

                return Ok(Resources.StatusMesg.S_Edit);

            }
            catch (EduValidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = department
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Xóa tổ hợp môn
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpDelete("{departmentID}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            var res = _departmentRepository.DeleteDepartmentByID(departmentId);
            return Ok(res);
        }
        #endregion
    }
}
