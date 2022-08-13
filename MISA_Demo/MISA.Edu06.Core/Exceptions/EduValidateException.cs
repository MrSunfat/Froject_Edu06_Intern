using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Exceptions
{
    public class EduValidateException : Exception
    {
        #region Properties
        string? MsgErrorValidate = null;
        #endregion

        #region Constructor
        public EduValidateException(string msg)
        {
            this.MsgErrorValidate = msg;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Nội dụng lỗi
        /// </summary>
        public override string Message
        {
            get { return MsgErrorValidate; }
        }
        #endregion
    }
}
