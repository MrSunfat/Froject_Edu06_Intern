using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;
using Dapper;
using MISA.Edu06.Core.DTO;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class TeacherRepository : BaseRepository, ITeacherRepository
    {
        #region Constructor
        public TeacherRepository() { }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra tất cả các giáo viên
        /// </summary>
        /// <returns>Thông tin của tất cả giáo viên</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public IEnumerable<Teacher> GetAll()
        {
            // Procedure lấy ra các thông của giáo viên
            var sqlCommand = "Proc_GetAllTeacher";

            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var teachers = FormatTeacher(_mySqlConnection, sqlCommand, new { });

                return teachers;
            }
        }

        /// <summary>
        /// Lấy giáo viên qua teacherID
        /// </summary>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Thông tin giáo viên qua mã ID</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public Teacher GetTeacherByID(Guid teacherID)
        {
            // Procedure lấy giáo viên qua teacherID
            var sqlCommand = "Proc_GetTeacherByID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@teacherID", teacherID);

            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var teachers = FormatTeacher(_mySqlConnection, sqlCommand, parameters);

                return teachers[0];
            }
        }

        /// <summary>
        /// Thêm mới giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Số lượng giáo viên thêm</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public int AddNewTeacher(Teacher teacher)
        {
            Guid newTeacherID = Guid.NewGuid();
            // Lệnh truy vấn thêm 
            var procAddTeacher = "Proc_AddNewTeacher";

            // Tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
            DynamicParameters parametersTeacher = new DynamicParameters();
            parametersTeacher.Add("@teacherID", newTeacherID);
            parametersTeacher.Add("@teacherCode", teacher.TeacherCode);
            parametersTeacher.Add("@fullName", teacher.FullName);
            parametersTeacher.Add("@email", teacher.Email);
            parametersTeacher.Add("@phoneNumber", teacher.PhoneNumber);
            parametersTeacher.Add("@departmentID", teacher.DepartmentID);
            parametersTeacher.Add("@isProfessionalQualifications", teacher.IsProfessionalQualifications);
            parametersTeacher.Add("@isWorking", teacher.IsWorking);
            parametersTeacher.Add("@dayOff", teacher.DayOff);

            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                _mySqlConnection.Open();
                using(MySqlTransaction transaction = _mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Thêm:
                        // 1. Thêm thông tin của giáo viên mới vào bảng Teacher
                        var res = _mySqlConnection.Execute(procAddTeacher, parametersTeacher, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        // 2. Thêm các môn học vào TeacherSubject (nếu có)
                        if (teacher.ListSubject.Count > 0)
                        {
                            var procAddTeacherSubject = "Proc_AddNewTeacherSubject";
                            foreach (var subject in teacher.ListSubject)
                            {
                                if (subject != null)
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@teacherSubjectID", Guid.NewGuid());
                                    parameters.Add("@teacherID", newTeacherID);
                                    parameters.Add("@subjectID", subject.SubjectID);

                                    _mySqlConnection.Query(procAddTeacherSubject, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                                }

                            }

                        }
                        // 3. Thêm các phòng thiết bị vào bảng TeacherEquimentRoom (nếu có)
                        if (teacher.ListRoom.Count > 0)
                        {
                            var procAddTeacherEquimentRoom = "Proc_AddTeacherEquimentRoom";
                            foreach (var room in teacher.ListRoom)
                            {
                                if (room != null)
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@teacherEquimentRoomID", Guid.NewGuid());
                                    parameters.Add("@teacherID", newTeacherID);
                                    parameters.Add("@equimentRoomID", room.EquimentRoomID);

                                    _mySqlConnection.Query(procAddTeacherEquimentRoom, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                                }
                            }
                        }

                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                
            }
        }

        /// <summary>
        /// Sửa thông tin giáo viên qua teacherID
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên thêm</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public int UpdateTeacherByID(Teacher teacher, Guid teacherID)
        {
            var procUpdateNewTeacher = "Proc_UpdateNewTeacher";

            Teacher teacherByID = GetTeacherByID(teacherID);

            if (teacher.TeacherCode == null)
            {
                teacher.TeacherCode = teacherByID.TeacherCode;
            }
            if (teacher.FullName == null)
            {
                teacher.FullName = teacherByID.FullName;
            }
            if (teacher.Email == null)
            {
                teacher.Email = teacherByID.Email;
            }
            if (teacher.PhoneNumber == null || teacher.PhoneNumber == "")
            {
                teacher.PhoneNumber = teacherByID.PhoneNumber;
            }
            if (teacher.DepartmentID == null)
            {
                teacher.DepartmentID = teacherByID.DepartmentID;
            }
            if (teacher.IsProfessionalQualifications == null)
            {
                teacher.IsProfessionalQualifications = teacherByID.IsProfessionalQualifications;
            }
            if (teacher.IsWorking == null)
            {
                teacher.IsWorking = teacherByID.IsWorking;
            }
            if (teacher.DayOff == null)
            {
                teacher.DayOff = teacherByID.DayOff;
            }

            // Tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
            DynamicParameters parametersTeacher = new DynamicParameters();
            parametersTeacher.Add("@teacherID", teacherID);
            parametersTeacher.Add("@teacherCode", teacher.TeacherCode);
            parametersTeacher.Add("@fullName", teacher.FullName);
            parametersTeacher.Add("@email", teacher.Email);
            parametersTeacher.Add("@phoneNumber", teacher.PhoneNumber);
            parametersTeacher.Add("@departmentID", teacher.DepartmentID);
            parametersTeacher.Add("@isProfessionalQualifications", teacher.IsProfessionalQualifications);
            parametersTeacher.Add("@isWorking", teacher.IsWorking);
            parametersTeacher.Add("@dayOff", teacher.DayOff);

            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                _mySqlConnection.Open();
                using(MySqlTransaction transaction = _mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Sửa:
                        // 1. Sửa thông tin của giáo viên mới vào bảng Teacher
                        var res = _mySqlConnection.Query<Teacher>(procUpdateNewTeacher, parametersTeacher, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        // 2. Sửa các môn học vào TeacherSubject (nếu có)
                        if (teacher.ListSubject != null)
                        {
                            //var procUpdateTeacherSubject = "Proc_UpdateTeacherSubject";
                            var procDeleteTeacherSubject = "Proc_DeleteTeacherSubjectByTeacherID";
                            var procAddTeacherSubject = "Proc_AddNewTeacherSubject";

                            _mySqlConnection.Execute(procDeleteTeacherSubject, parametersTeacher, transaction, commandType: System.Data.CommandType.StoredProcedure);

                            foreach (var subject in teacher.ListSubject)
                            {
                                if (subject != null)
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@teacherSubjectID", Guid.NewGuid());
                                    parameters.Add("@teacherID", teacherID);
                                    parameters.Add("@subjectID", subject.SubjectID);

                                    _mySqlConnection.Execute(procAddTeacherSubject, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                                }
                            }

                        }

                        // 3. Sửa các phòng thiết bị vào bảng TeacherEquimentRoom (nếu có)
                        if (teacher.ListRoom != null)
                        {
                            //var procUpdateTeacherEquimentRoom = "Proc_UpdateTeacherEquimentRoom";
                            var procDeleteTeacherEquimentRoom = "Proc_DeleteTeacherEquimentRoomByID";
                            var procAddTeacherEquimentRoom = "Proc_AddTeacherEquimentRoom";

                            _mySqlConnection.Execute(procDeleteTeacherEquimentRoom, parametersTeacher, transaction, commandType: System.Data.CommandType.StoredProcedure);

                            foreach (var room in teacher.ListRoom)
                            {
                                if (room != null)
                                {
                                    DynamicParameters parameters = new DynamicParameters();
                                    parameters.Add("@teacherEquimentRoomID", Guid.NewGuid());
                                    parameters.Add("@teacherID", teacherID);
                                    parameters.Add("@equimentRoomID", room.EquimentRoomID);

                                    _mySqlConnection.Execute(procAddTeacherEquimentRoom, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                                }
                            }
                        }

                        transaction.Commit();

                        return res.Count();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                
            }
        }

        /// <summary>
        /// Xóa teacher qua teacherID
        /// </summary>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên xóa</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public int DeleteTeacherByID(Guid teacherID)
        {
            // Procedure : 
            // Của bảng Teacher
            var procDeleteTeacher = "Proc_DeleteTeacherByID";
            // Của bảng TeacherSubject
            var procDeleteTeacherSubject = "Proc_DeleteTeacherSubjectByTeacherID";
            // Của bảng TeacherEquimentRoom
            var procDeleteTeacherEquimentRoom = "Proc_DeleteTeacherEquimentRoomByID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@teacherID", teacherID);
            // Kết nối database:
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                // thực hiện xóa:
                // 1. Xóa Giáo viên ở bảng Teacher
                var res = _mySqlConnection.Execute(procDeleteTeacher, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 2. Xóa thông tin môn học của giáo viên đó ở bảng TeacherSubject
                _mySqlConnection.Execute(procDeleteTeacherSubject, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 3. Xóa thông tin phòng thiết bị của giáo viên đó ở bảng TeacherEquimentRoom
                _mySqlConnection.Execute(procDeleteTeacherEquimentRoom, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        
        /// <summary>
        /// Format trường ListSubject, ListRoom, DepartmentID, DepartmentName của teacher
        /// </summary>
        /// <param name="connection">Kết nối DB</param>
        /// <param name="sqlCommand">Câu lệnh</param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public static List<Teacher> FormatTeacher(MySqlConnection connection, string sqlCommand, object values)
        {
            // Tạo teacherDictionary để lưu các teacher đã tồn tại
            var teacherDictionary = new Dictionary<Guid, Teacher>();

            // Truy vấn database
            var teachers = connection.Query<Teacher, Subject, EquimentRoom, Department, Teacher>(
                sqlCommand,
                (teacher, subject, equimentRoom, department) =>
                {
                    Teacher teacherEntry;

                    // Kiểm tra teacher này đã tồn tại chưa ?
                    // Nếu chưa tồn tại thì thêm vào teacherDictionary
                    if (!teacherDictionary.TryGetValue(teacher.TeacherID, out teacherEntry))
                    {
                        teacherEntry = teacher;
                        teacherEntry.ListSubject = new List<Subject>();
                        teacherEntry.ListRoom = new List<EquimentRoom>();
                        teacherDictionary.Add(teacherEntry.TeacherID, teacherEntry);
                    }

                    // kiểm tra môn học khác null thì thêm vào ListSubject và kiểm tra subject đã có chưa
                    if (subject != null && !teacherEntry.ListSubject.Any(s => s.SubjectID == subject.SubjectID))
                    {
                        teacherEntry.ListSubject.Add(subject);
                    }

                    // kiểm tra phòng thiết bị khác null thì thêm vào ListRooms và kiểm tra room đã có chưa
                    if (equimentRoom != null && !teacherEntry.ListRoom.Any(r => r.EquimentRoomID == equimentRoom.EquimentRoomID))
                    {
                        teacherEntry.ListRoom.Add(equimentRoom);
                    }
                    // kiểm tra tổ hợp môn khác null -> set giá trị cho thông tin tổ hợp vào teacher
                    if (department != null)
                    {
                        teacherEntry.DepartmentID = department.DepartmentID;
                        teacherEntry.DepartmentName = department.DepartmentName;
                    }
                    return teacherEntry;
                },
                splitOn: "TeacherID, SubjectID, EquimentRoomID, DepartmentID",
                param: values,
                commandType: System.Data.CommandType.StoredProcedure)
            .Distinct()
            .ToList();

            return teachers;
        }

        /// <summary>
        /// Xóa nhiều giáo viên qua danh sách teacherID
        /// </summary>
        /// <param name="listTeacherID">Danh sách các teacherID</param>
        /// <returns>Trả về trạng thái</returns>
        /// CreatedBy: TNDanh (8/8/2022)
        public int DeleteListTeacher(Guid[] listTeacherID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                _mySqlConnection.Open();
                using(MySqlTransaction transaction = _mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var listID = "";
                        // Lưu ý: nếu có tham số truyền trong câu lệnh truy vấn sql thì phải sử dụng DynamicParameter
                        DynamicParameters parameters = new DynamicParameters();
                        for (int i = 0; i < listTeacherID.Length; i++)
                        {
                            string param = $"@param{i}";
                            if (i < listTeacherID.Length - 1)
                            {
                                listID += param + ",";
                            }
                            else
                            {
                                listID += param;
                            } 

                            parameters.Add(param, listTeacherID[i]);
                        }

                        //parameters.Add("@listID", string.Join(",", listTeacherID));
                        // Xóa các giáo viên qua listID
                        var sqlDeleteTeachers = $"DELETE FROM Teacher WHERE TeacherID IN ({listID})";
                        var res = _mySqlConnection.Execute(sql: sqlDeleteTeachers, param: parameters, transaction);
                        // Xóa các môn học của giáo viên đó
                        var sqlDeleteTeacherSubject = $"DELETE FROM teachersubject WHERE TeacherID IN ({listID})";
                        _mySqlConnection.Execute(sql: sqlDeleteTeacherSubject, param: parameters, transaction);
                        // Xóa các phòng học của giáo viên đó
                        var sqlDeleteTeacherRoom = $"DELETE FROM teacherequimentroom WHERE TeacherID IN ({listID})";
                        _mySqlConnection.Execute(sql: sqlDeleteTeacherRoom, param: parameters, transaction);

                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                } 
            }
        }

        /// <summary>
        /// Tạo một teacherCode mới
        /// </summary>
        /// <returns>Mã giáo viên mới</returns>
        /// CreatedBy: TNDanh (8/8/2021)
        public string NewTeacherCode()
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = "SELECT MAX(t.TeacherCode) FROM Teacher t;";
                var res = _mySqlConnection.QueryFirstOrDefault<string>(sql: sqlCommand);
                string[] words = res.Split("-");
                res = words[0] + "-" + (Convert.ToInt32(words[1]) + 1);
                return res;
            }
        }

        /// <summary>
        /// Kiểm tra mã giáo viên có trùng không ?
        /// </summary>
        /// <param name="teacherCode"></param>
        /// <returns></returns>
        public bool CheckTeacherCodeDuplicate(string teacherCode)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlCommand = "SELECT t.TeacherCode FROM Teacher t WHERE t.TeacherCode = @teacherCode;";
                DynamicParameters param = new DynamicParameters();
                param.Add("@teacherCode", teacherCode);
                var res = _mySqlConnection.QueryFirstOrDefault<string>(sqlCommand, param: param);
                
                if (res != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Lọc các giáo viên bằng từ khóa và phân trang
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (11/8/2022)
        public object FilterTeacher(string? search, int pageSize, int pageNumber)
        {
            var sqlFilterAndPaging = "Proc_GetPaging";
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pageNumber", pageNumber);
                parameters.Add("@pageSize", pageSize);
                parameters.Add("@search", search);
                parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                //Console.WriteLine(whereString);

                var res = FormatTeacher(_mySqlConnection, sqlFilterAndPaging, parameters);

                var response = new
                {
                    TotalRecord = parameters.Get<int>("@totalRecord"),
                    TotalPage = parameters.Get<int>("@totalPage"),
                    CurrentPage = pageNumber,
                    CurrentPageRecords = pageSize,
                    Data = res,
                };

                return response;
            }
        }
        #endregion
    }
}
