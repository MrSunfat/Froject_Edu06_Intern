using Dapper;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        public IEnumerable<Department> GetAll()
        {
            using(_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = "SELECT * FROM department";
                var res = _mySqlConnection.Query<Department>(sqlCommand);
                return res;
            }
        }

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
    }
}
