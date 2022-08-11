using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Interfaces.Services
{
    public interface ISubjectService
    {
        #region Kiểm tra tên  môn học và ID của tổ hợp môn
        public int InsertService(Subject subject);
        public int UpdateService(Subject subject);
        #endregion
    }
}
