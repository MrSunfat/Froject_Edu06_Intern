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
        public int InsertService(Subject subject)
        {
            // Validate
            List<string> error = new List<string>();
            // 1. Tên môn học
            // 1a. Có khác rỗng không ?
            if (string.IsNullOrEmpty(subject.SubjectName) == true)
            {
                error.Add(Resources.Content.subjectNameNotEmpty);
            }

            if (error.Count > 0)
            {
                string errContent = String.Join(" - ", error);
                throw new EduValidateException(errContent);
            }
            return 1;
        }

        public int UpdateService(Subject subject)
        {
            List<string> error = new List<string>();
            // 1a. Có khác rỗng không ?
            if (string.IsNullOrEmpty(subject.SubjectName) == true)
            {
                error.Add(Resources.Content.subjectNameNotEmpty);
            }

            if (error.Count > 0)
            {
                string errContent = String.Join(" - ", error);
                throw new EduValidateException(errContent);
            }
            return 1;
        }
    }
}
