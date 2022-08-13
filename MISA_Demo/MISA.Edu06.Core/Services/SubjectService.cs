using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Services
{
    public class SubjectService : ISubjectService
    {
        #region Methods
        /// <summary>
        /// Validate thông tin môn học trước khi thêm
        /// </summary>
        /// <param name="subject">Thông tin môn học</param>
        /// <returns></returns>
        /// <exception cref="EduValidateException">Thông báo lỗi</exception>
        /// CreatedBy: TNDanh (14/8/2022)
        public int InsertService(Subject subject)
        {
            // Validate
            List<string> error = new List<string>();
            // 1. Tên môn học
            // 1a. Có khác rỗng không ?
            if (string.IsNullOrEmpty(subject.SubjectName) == true)
            {
                error.Add(Resources.Content.E_SubjectNameNotEmpty);
            }

            if (error.Count > 0)
            {
                string errContent = String.Join(" - ", error);
                throw new EduValidateException(errContent);
            }
            return 1;
        }

        /// <summary>
        /// Validate thông tin môn học trước khi sửa
        /// </summary>
        /// <param name="subject">Thông tin môn học</param>
        /// <returns></returns>
        /// <exception cref="EduValidateException">Thông báo lỗi</exception>
        /// CreatedBy: TNDanh (14/8/2022)
        public int UpdateService(Subject subject)
        {
            List<string> error = new List<string>();
            // 1a. Có khác rỗng không ?
            if (string.IsNullOrEmpty(subject.SubjectName) == true)
            {
                error.Add(Resources.Content.E_SubjectNameNotEmpty);
            }

            if (error.Count > 0)
            {
                string errContent = String.Join(" - ", error);
                throw new EduValidateException(errContent);
            }
            return 1;
        }
        #endregion
    }
}
