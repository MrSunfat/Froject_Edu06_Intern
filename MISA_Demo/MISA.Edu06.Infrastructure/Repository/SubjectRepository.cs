using Dapper;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        public IEnumerable<Subject> GetAll()
        {
            using(_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlGetAll = "SELECT * FROM subject";
                var res = _mySqlConnection.Query<Subject>(sqlGetAll);
                return res;
            }
        }

        public int AddNewSubject(Subject subject)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                // 1. Tạo ra 1 ID môn học mới
                var sqlFindMaxSubjectID = "SELECT MAX(s.SubjectID) FROM subject s;";
                var newSubjectID = _mySqlConnection.ExecuteScalar<int>(sqlFindMaxSubjectID);
                // 2. Thực hiện truy vấn
                var sqlAddSubject = 
                    "INSERT subject (SubjectID, SubjectName, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)"
                    + " VALUES(@subjectID, @subjectName, '', NOW(), '', NOW()); ";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@subjectID", newSubjectID + 1);
                parameters.Add("@subjectName", subject.SubjectName);

                var res = _mySqlConnection.Execute(sqlAddSubject, param: parameters);
                return res;
            }
        }

        public int UpdateSubjectByID(Subject subject, int subjectID)
        {
            using(_mySqlConnection =new MySqlConnection(connectString))
            {
                // 1. Tìm giá trị hiện tại trong môn học đó
                var sqlGetSubjectByID = "SELECT * FROM subject WHERE SubjectID = @subjectID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@subjectID", subjectID);
                var subjectByID = _mySqlConnection.QueryFirstOrDefault<Subject>(sqlGetSubjectByID, param: parameters);
                // 2. Kiểm tra các thuộc tính trong subject
                // 2a. Tên môn
                if (string.IsNullOrEmpty(subject.SubjectName) == true)
                {
                    subject.SubjectName = subjectByID.SubjectName;
                }
                // 3. Cập nhật giá trị mới của môn học đó
                var sqlEditSubject = 
                    "UPDATE subject s"
                    + " SET s.SubjectName = @subjectName"
                    + " WHERE s.SubjectID = @subjectID;";

                parameters.Add("@subjectName", subject.SubjectName);

                var res = _mySqlConnection.Execute(sqlEditSubject, param: parameters);
                return res;
            }
        }

        public int DeleteSubjectByID(int subjectID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlDeleteSubject = "DELETE FROM subject WHERE subject.SubjectID = @subjectID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@subjectID", subjectID);

                var res = _mySqlConnection.Execute(sqlDeleteSubject, param: param);
                return res;
            }
        }
    }
}
