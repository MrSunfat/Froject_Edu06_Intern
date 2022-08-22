using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Edu06.Core.DTO;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;
using MISA.Edu06.Core.Services;
using MISA.Edu06.Infrastructure.Repository;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Drawing;

namespace MISA.Edu06.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        #region Properties
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherService _teacherService;
        public static IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor
        public TeacherController(ITeacherRepository teacherRepository, ITeacherService teacherService, IWebHostEnvironment webHostEnvironment)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra thông tin tất cả giáo viên
        /// </summary>
        /// <returns>Thông tin tất cả giáo viên</returns>
        /// CreatedBy: TNDanh (13/8/2022)
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
        /// <param name="teacherID">Mã ID của giáo viên đó</param>
        /// <returns>Thông tin giáo viên</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpGet("{teacherID}")]
        public IActionResult GetTeacherByID(Guid teacherID)
        {
            try
            {
                // Thực hiện lấy dữ liệu qua teacherID
                var teacher = _teacherRepository.GetTeacherByID(teacherID);

                string avatarName = "TeacherPic_" + teacher.TeacherCode + ".png";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "TeacherPics", avatarName);
                if (System.IO.File.Exists(path))
                {
                    teacher.ImgByte = System.IO.File.ReadAllBytes(path); 
                }

                return Ok(teacher);

            } catch(Exception ex)
            {
                var error = new
                {
                    statusCode = 400,
                    ex = ex.Message,
                    message = Resources.StatusMesg.E_FindNotFound,
                    devMes = Resources.StatusMesg.E_FindNotFoundDev,
                };
                return Ok(error);
            }

        }

        /// <summary>
        /// Tạo ra một mã giáo viên mới
        /// </summary>
        /// <returns>Tạo ra mã giáo viên mới</returns>
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
        /// <param name="search">Từ tìm kiếm tên hoặc mã giáo viên</param>
        /// <param name="pageSize">Số lượng giáo viên</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Thông tin các giáo viên</returns>
        /// CreatedBy: TNDanh (10/8/2022)
        [HttpGet("Filter")]
        public IActionResult FilterTeacher([FromQuery]string? search, [FromQuery]int pageSize = 20, [FromQuery]int pageNumber = 1)
        {
            var res = _teacherRepository.FilterTeacher(search, pageSize, pageNumber);
            return Ok(res);
        }

        /// <summary>
        /// Xuất file
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Thông tin của các giáo viên</returns>
        /// CreatedBy: TNDanh (16/8/2022)
        [HttpGet("Export")]
        public async Task<IActionResult> ExportTeacher(CancellationToken cancellationToken)
        {
            await Task.Yield();
            // Lấy danh sách giáo viên
            List<TeacherDTO> teachers = _teacherRepository.GetAll().ToList();

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                // Tạo 1 sheet excel
                var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH GIÁO VIÊN");
                var rowStart = 1;
                var rowEnd = teachers.Count + rowStart;

                // Biến lưu giá trị cột số thứ tự
                var tableIndex = 1;
                workSheet.Cells[rowStart, 1].Value = "STT";
                workSheet.Cells[rowStart, 2].Value = "Số hiệu cán bộ";
                workSheet.Cells[rowStart, 3].Value = "Họ và tên";
                workSheet.Cells[rowStart, 4].Value = "Số điện thoại";
                workSheet.Cells[rowStart, 5].Value = "Tổ chuyên môn";
                workSheet.Cells[rowStart, 6].Value = "Quản lý theo môn";
                workSheet.Cells[rowStart, 7].Value = "Quản lý kho, phòng";
                workSheet.Cells[rowStart, 8].Value = "Đào tạo QLTB";
                workSheet.Cells[rowStart, 9].Value = "Đang làm việc";

                // set nội dung (thông tin tất cả nhân viên) cho bảng của file excel (+1 vào rowStart để bỏ qua header)
                var forLoopIndex = rowStart + 1;
                foreach (var tea in teachers)
                {
                    workSheet.Cells[forLoopIndex, 1].Value = tableIndex;
                    workSheet.Cells[forLoopIndex, 2].Value = tea.TeacherCode;
                    workSheet.Cells[forLoopIndex, 3].Value = tea.FullName;
                    workSheet.Cells[forLoopIndex, 4].Value = tea.PhoneNumber;
                    workSheet.Cells[forLoopIndex, 5].Value = tea.DepartmentName;
                    workSheet.Cells[forLoopIndex, 6].Value = tea.SubjectName;
                    workSheet.Cells[forLoopIndex, 7].Value = tea.Room;
                    if (tea.IsProfessionalQualifications == true)
                    {
                        workSheet.Cells[forLoopIndex, 8].Value = "x";
                        workSheet.Cells[forLoopIndex, 8].Style.Font.Color.SetColor(Color.Red);
                    }
                    if (tea.IsWorking == true)
                    {
                        workSheet.Cells[forLoopIndex, 9].Value = "x";
                        workSheet.Cells[forLoopIndex, 9].Style.Font.Color.SetColor(Color.Red);
                    }
                    forLoopIndex++;
                    tableIndex++;
                }

                // tạo table và các cài đặt
                ExcelTable tbl = workSheet.Tables.Add(workSheet.Cells[rowStart, 1, rowEnd, 9], "TeacherList");
                // add border
                workSheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rowStart, 1, rowEnd, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                // hiện header
                tbl.ShowHeader = true;

                //Xét font style cho table
                for (int i = 1; i <= 9; i++)
                {
                    workSheet.Column(i).Style.Font.Size = 11;
                    workSheet.Column(i).Style.Font.Name = "Times New Roman";
                    workSheet.Column(i).Style.Font.Color.SetColor(Color.Black);

                    workSheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.DarkGrid;
                    workSheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                }

                //Xét font style cho title
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1].Style.Font.Name = "Arial";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.Font.Color.SetColor(Color.Black);

                //Xét font style cho header
                workSheet.Row(1).Style.Font.Size = 10;
                workSheet.Row(1).Style.Font.Name = "Arial";
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Font.Color.SetColor(Color.Black);

                // Thêm style cho toàn bộ table
                tbl.TableStyle = TableStyles.Light8;

                // Căn giữa cho header và các dòng theo yêu câu
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Columns[8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Columns[9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // AutoFit Columns
                workSheet.Cells[rowStart, 1, rowEnd, 9].AutoFitColumns();

                //Ẩn data filter
                tbl.ShowFilter = false;

                package.Save();
            }
            stream.Position = 0;
            string excelName = "Danh sách giáo viên.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        /// <summary>
        /// Thêm giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Số lượng thêm giáo viên</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        [HttpPost()]
        public IActionResult AddNewTeacher([FromForm]Teacher teacher)
        {   
            try
            {
                var files = teacher.Files;
                //teacher.Files = null;

                // Validate dữ liệu
                var resAfterValidate = _teacherService.InsertService(teacher);

                if (teacher.TeacherCode != null && files != null && files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\TeacherPics\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string avatarName = "TeacherPic_" + teacher.TeacherCode + ".png";
                    if (System.IO.File.Exists(path + avatarName))
                    {
                        System.IO.File.Delete(path + avatarName);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + avatarName))
                    {
                        files.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                // Thêm một giáo viên mới
                var res = _teacherRepository.AddNewTeacher(teacher);

                var response = new
                {
                    statusCode = 200,
                    devMsg = Resources.StatusMesg.S_Post,
                    userMsg = Resources.StatusMesg.S_Post
                };

                return Ok(response);

            }
            catch(EduValidateException ex)
            {
                var response = new
                {
                    statusCode = 400,
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    Data = teacher
                };
                return Ok(response);    
            }
            catch (Exception ex)
            {
                var response = new
                {
                    statusCode = 400,
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    Data = teacher
                };
                return Ok(response);
            }

        }

        /// <summary>
        /// Sửa thông tin giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin mới của giáo viên đó</param>
        /// <param name="teacherID">Mã ID của giáo viên đó</param>
        /// <returns>Số lượng giáo viên sửa</returns>
        /// CreatedBy: TNDanh (4/8/2022)
        [HttpPut("{teacherID}")]
        public IActionResult EditTeacher(Teacher teacher, Guid teacherID)
        {
            var res = _teacherRepository.UpdateTeacherByID(teacher, teacherID);
            return Ok(res);
        }

        /// <summary>
        /// Upload avatar của giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Trạng thái lưu ảnh thành công hay thất bại</returns>
        /// CreatedBy: TNDanh (21/8/2022)
        [HttpPost("Upload")]
        public IActionResult UploadImage([FromForm]Teacher teacher)
        {
            try
            {
                var files = teacher.Files;
                teacher.Files = null;

                if (teacher.TeacherCode != null && files != null && files.Length > 0)
                {
                    // Tạo đường dẫn Foler để lưu ảnh
                    string path = _webHostEnvironment.WebRootPath + "\\TeacherPics\\";
                    // Kiểm tra Folder chưa tồn tại
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    // Tạo đường dẫn của ảnh
                    string avatarName = "TeacherPic_" + teacher.TeacherCode + ".png";
                    // Kiểm tra ảnh chưa tồn tại
                    if (System.IO.File.Exists(path + avatarName))
                    {
                        System.IO.File.Delete(path + avatarName);
                    }
                    // Thực hiện lưu ảnh
                    using (FileStream fileStream = System.IO.File.Create(path + avatarName))
                    {
                        files.CopyTo(fileStream);
                        fileStream.Flush();

                        return Ok(path + avatarName);
                    }
                }
                else
                {
                    return Ok("Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// Xóa teacher qua teacherID
        /// </summary>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên xóa</returns>
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
                    message = Resources.StatusMesg.E_FindNotFound,
                    devMes = Resources.StatusMesg.E_FindNotFoundDev,
                };
                return StatusCode(400, error);
            }
        }

        /// <summary>
        ///  Xóa nhiều giáo viên qua teacherID từ body
        /// </summary>
        /// <param name="listTeacher">Danh sách teacherID</param>
        /// <returns>Số lượng giáo viên xóa</returns>
        [HttpDelete]
        public IActionResult DeleteListTeacher([FromBody] Guid[] listTeacher)
        {
            try
            {
                var res = _teacherRepository.DeleteListTeacher(listTeacher);
                return StatusCode(StatusCodes.Status200OK, Resources.StatusMesg.S_Delete);
            } catch(Exception ex)
            {
                var error = new
                {
                    ex = ex.Message,
                    message = Resources.StatusMesg.E_FindNotFound,
                    devMes = Resources.StatusMesg.E_FindNotFoundDev,
                };
                return BadRequest(error);
            }
        }
        #endregion
    }
}
