using Dapper;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        #region Methods
        /// <summary>
        /// Lấy tất cả thông tin tổ hợp môn
        /// </summary>
        /// <returns>Tất cả thông tin của tổ hợp môn</returns>
        /// CreatedBy: TNDanh (14/8/2022)
        public IEnumerable<Department> GetAll()
        {
            using(_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = "SELECT * FROM department";
                var res = _mySqlConnection.Query<Department>(sqlCommand);
                return res;
            }
        }

        /// <summary>
        /// Thêm tổ hợp môn mới
        /// </summary>
        /// <param name="department">Thông tin của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn</returns>
        /// CreatedBy: TNDanh (14/8/2022)
        public int AddNewDepartment(Department department)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlAddDepartment = 
                    "INSERT department (DepartmentID, DepartmentName, CreatedBy, CreatedDate, ModifyBy, ModifyDate)"
                    + "VALUES(@departmentID, @departmentName, '', NOW(), '', NOW()); ";

                var sqlFindMaxDepartmentID = "SELECT MAX(d.DepartmentID) FROM department d;";

                var newDepartmentID = _mySqlConnection.ExecuteScalar<int>(sqlFindMaxDepartmentID);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@departmentID", newDepartmentID + 1);
                parameters.Add("@departmentName", department.DepartmentName);

                var res = _mySqlConnection.Execute(sqlAddDepartment, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Sửa tổ hợp môn qua departmentID
        /// </summary>
        /// <param name="department">Thông tin mới của tổ hợp môn</param>
        /// <param name="departmentID">Mã ID của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn sửa</returns>
        public int UpdateDepartmentByID(Department department, int departmentID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlUpdateDepartment = "UPDATE department d SET d.DepartmentName = @departmentName WHERE d.DepartmentID = @departmentID;";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@departmentName", department.DepartmentName);
                parameters.Add("@departmentID", departmentID);

                var res = _mySqlConnection.Execute(sqlUpdateDepartment, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Xóa giáo viên qua departmentID
        /// </summary>
        /// <param name="departmentID">Mã ID của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn xóa</returns>
        /// CreatedBy: TNDanh (14/8/2022)
        public int DeleteDepartmentByID(int departmentID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlDeleteDepartment = "DELETE FROM department WHERE department.DepartmentID = @departmentID;";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@departmentID", departmentID);

                var res = _mySqlConnection.Execute(sqlDeleteDepartment, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Kiểm tra tên tổ hợp môn có trùng không ?
        /// </summary>
        /// <param name="departmentName">Mã ID của tổ hợp môn</param>
        /// <returns>True - Trùng, False - Không trùng</returns>
        /// CreatedBy: TNDanh (14/8/2022)
        public bool CheckDepartmentNameDuplicate(string departmentName)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = "SELECT d.DepartmentName FROM department d WHERE d.DepartmentName = @departmentName;";

                DynamicParameters param = new DynamicParameters();
                param.Add("@departmentName", departmentName);

                var res = _mySqlConnection.QueryFirstOrDefault<string>(sqlCommand, param: param);
                if (res != null)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
