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
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectService _subjectService;


        public SubjectController(ISubjectRepository subjectRepository, ISubjectService subjectService)
        {
            _subjectRepository = subjectRepository;
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _subjectRepository.GetAll();
            return Ok(res);
        }

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

        [HttpDelete("{subjectID}")]
        public int DeleteSubjectByID(int subjectID)
        {
            var res = _subjectRepository.DeleteSubjectByID(subjectID);
            return res;
        }
    }
}
