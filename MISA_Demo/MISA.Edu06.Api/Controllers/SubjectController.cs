using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;

namespace MISA.Edu06.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        #region Properties
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectService _subjectService;
        #endregion

        #region Constructor
        public SubjectController(ISubjectRepository subjectRepository, ISubjectService subjectService)
        {
            _subjectRepository = subjectRepository;
            _subjectService = subjectService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả môn học 
        /// </summary>
        /// <returns>Tất cả môn học</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _subjectRepository.GetAll();
            return Ok(res);
        }

        /// <summary>
        /// Thêm môn học mới
        /// </summary>
        /// <param name="subject">Thông tin của môn học mới</param>
        /// <returns>Số lượng môn học thêm mới</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpPost]
        public IActionResult AddNewSubject(Subject subject)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _subjectService.InsertService(subject);
                // Thêm một môn học mới
                var res = _subjectRepository.AddNewSubject(subject);

                var response = new
                {
                    devMsg = "Success Post",
                    userMsg = "Success Post"
                };

                return Ok(response);

            }
            catch (EduValidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = subject
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Sửa môn học qua subjectID
        /// </summary>
        /// <param name="subject">Thông tin mới của môn học</param>
        /// <param name="subjectID">Mã ID của môn học</param>
        /// <returns>Số lượng môn học sửa</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpPut("{subjectID}")]
        public IActionResult EditSubjectByID(Subject subject, int subjectID)
        {
            try
            {
                // Validate dữ liệu
                var resAfterValidate = _subjectService.UpdateService(subject);
                // Thêm một môn học mới
                var res = _subjectRepository.UpdateSubjectByID(subject, subjectID);

                var response = new
                {
                    devMsg = "Success Post",
                    userMsg = "Success Post"
                };

                return Ok(response);

            }
            catch (EduValidateException ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = subject
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Xóa môn học qua subjectID
        /// </summary>
        /// <param name="subjectID">Mã ID của môn học</param>
        /// <returns>Số lượng môn xóa</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpDelete("{subjectID}")]
        public int DeleteSubjectByID(int subjectID)
        {
            var res = _subjectRepository.DeleteSubjectByID(subjectID);
            return res;
        }
        #endregion
    }
}
