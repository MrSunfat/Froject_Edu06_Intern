using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Entities
{
    public class Department
    {
        #region Properties

        /// <summary>
        /// ID của tổ hợp môn 
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// tên của tổ hợp môn 
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
