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
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _departmentRepository.GetAll();
            return Ok(res);
        }

        [HttpPost()]
        public IActionResult AddNewDepartment(Department department)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _departmentService.ValidateService(department);
                // Thêm một giáo viên mới
                var res = _departmentRepository.AddNewDepartment(department);

                return Ok("Success Create");

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

        [HttpPut("{departmentID}")]
        public IActionResult EditDepartment(Department department, int departmentID)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _departmentService.ValidateService(department);
                // Thêm một giáo viên mới
                var res = _departmentRepository.UpdateDepartmentByID(department, departmentID);

                return Ok("Success Edit");

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

        [HttpDelete("{departmentID}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            var res = _departmentRepository.DeleteDepartmentByID(departmentId);
            return Ok(res);
        }
        #endregion
    }
}
