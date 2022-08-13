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
        #region Methods
        /// <summary>
        /// Validate môn học trước khi thêm vào
        /// </summary>
        /// <param name="subject">Thông tin của môn học</param>
        /// <returns>Số lượng môn học validate</returns>
        /// CreaedBy: TNDanh (13/8/2022)
        public int InsertService(Subject subject);
        /// <summary>
        /// Validate môn học trước khi sửa
        /// </summary>
        /// <param name="subject">Thông tin mới của môn học</param>
        /// <returns>Số lượng môn học validate</returns>
        /// CreaedBy: TNDanh (13/8/2022)
        public int UpdateService(Subject subject);
        #endregion
    }
}
