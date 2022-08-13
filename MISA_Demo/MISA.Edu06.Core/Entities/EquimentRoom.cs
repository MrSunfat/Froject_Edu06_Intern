using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Entities
{
    public class EquimentRoom
    {
        #region Properties
        /// <summary>
        /// ID của phòng thiết bị 
        /// </summary>
        public int EquimentRoomID { get; set; }
 
        /// <summary>
        /// Tên phòng thiết bị 
        /// </summary>
        public string EquimentRoomName { get; set; }
        #endregion
    }
}
