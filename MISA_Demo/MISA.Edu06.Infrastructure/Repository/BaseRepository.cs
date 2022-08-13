using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class BaseRepository
    {
        #region Properties
        protected readonly string connectString = "Host=localhost; Port=3307; Database = Misa_web06_tndanh; User Id = root; Password = Jigsaw12345678";
        protected MySqlConnection _mySqlConnection;
        #endregion
    }
}
