using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Exceptions
{
    public class EduValidateException : Exception
    {
        string? MsgErrorValidate = null;

        public EduValidateException(string msg)
        {
            this.MsgErrorValidate = msg;
        }

        public override string Message
        {
            get { return MsgErrorValidate; }
        }
    }
}
