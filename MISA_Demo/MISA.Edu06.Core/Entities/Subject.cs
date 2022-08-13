using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Entities
{
    public class Subject
    {
        #region Properties
        /// <summary>
        /// ID của môn học 
        /// </summary>
        public int SubjectID { get; set; }
     
        /// <summary>
        /// tên môn học 
        /// </summary>
        public string SubjectName { get; set; }
        #endregion
    }
}
