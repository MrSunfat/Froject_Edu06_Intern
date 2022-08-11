using Dapper;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Infrastructure.Repository
{
    public class EquimentRoomRepository : BaseRepository, IEquimentRoomRepository
    {
        public IEnumerable<EquimentRoom> GetAll()
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlGetAll = "SELECT * FROM equimentroom";
                var res = _mySqlConnection.Query<EquimentRoom>(sqlGetAll);
                return res;
            }
        }

        public int AddNewRoom(EquimentRoom room)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                // Tạo ra ID của phòng học mới
                var sqlNewRoomID = "SELECT MAX(e.EquimentRoomID) FROM equimentroom e;";
                var newRoomID = _mySqlConnection.QueryFirstOrDefault<int>(sqlNewRoomID);
                // Thực thi tạo một phòng học mới
                var sqlAddRoom =
                    "INSERT equimentroom (EquimentRoomID, EquimentRoomName, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)"
                    + "VALUES(@roomID, @roomName, '', NOW(), '', NOW());";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@roomID", newRoomID + 1);
                parameters.Add("@roomName", room.EquimentRoomName);

                var res = _mySqlConnection.Execute(sqlAddRoom, param: parameters);

                return res;
            }
        }
        public int UpdateRoomByID(EquimentRoom room, int roomID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlEditRoom = "UPDATE equimentroom e SET e.EquimentRoomName = @roomName WHERE e.EquimentRoomID = @roomID;";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@roomName", room.EquimentRoomName);
                parameters.Add("@roomID", roomID);

                var res = _mySqlConnection.Execute(sqlEditRoom, param: parameters);
                return res;

            }
        }

        public int DeleteRoomByID(int roomID)
        {
            using (_mySqlConnection = new MySqlConnection(connectString))
            {
                var sqlEditRoom = "DELETE FROM equimentroom WHERE EquimentRoomID = @roomID;";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@roomID", roomID);

                var res = _mySqlConnection.Execute(sqlEditRoom, param: parameters);
                return res;
            }
        }

        public bool CheckRoomNameDuplicate(string departmentName)
        {
            throw new NotImplementedException();
        }
    }
}
