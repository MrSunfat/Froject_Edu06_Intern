using Dapper;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        #region Methods
        /// <summary>
        /// Lấy tất cả thông tin của môn học
        /// </summary>
        /// <returns>Tất cả thông tin của môn học</returns>
        /// CreatedBy: TNDanh (14/8/2022)
        public IEnumerable<Subject> GetAll()
        {
            using(_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlGetAll = "SELECT * FROM subject";
                var res = _mySqlConnection.Query<Subject>(sqlGetAll);
                return res;
            }
        }

        /// <summary>
        /// Thêm môn học mới
        /// </summary>
        /// <param name="subject">Thông tin môn học mới</param>
        /// <returns>Số lượng thêm môn mới</returns>
        /// CreatedBy: TNDanh (14/8/2022)
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

        /// <summary>
        /// Sửa môn học qua subjectID
        /// </summary>
        /// <param name="subject">Thông tin mới của môn</param>
        /// <param name="subjectID">Mã ID của môn</param>
        /// <returns>Số lượng môn sửa</returns>
        /// CreatedBy: TNDanh (14/8/2022)
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

        /// <summary>
        /// Xóa môn qua subjectID
        /// </summary>
        /// <param name="subjectID">Mã ID của môn</param>
        /// <returns>Số lượng của môn xóa</returns>
        /// CreatedBy: TNDanh (14/8/2022)
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
        #endregion
    }
}
