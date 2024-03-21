using BusinessObject;
using BusinessObject.Enum;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class AttendaceReposiory : IAttendanceRepository
    {
        public void DeleteAttendance(Attendance Attendance)
        {
            throw new NotImplementedException();
        }

        public Attendance FindAttendanceById(int attendanceId) => AttendanceDAO.FindAttendanceById(attendanceId);

        public List<Attendance> FindAttendanceByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Attendance FindAttendanceByUserAndDay(int userId, DateTime time)
        {
            return AttendanceDAO.FindAttendanceByUserAndDay(userId, time);
        }

        public List<Attendance> FindAttendanceByUserAndTime(int userId, DateTime timeBegin, DateTime timeEnd)
        {
             return AttendanceDAO.FindAttendanceByUserAndTime(userId, timeBegin, timeEnd);
        }

        public List<Attendance> GetAttendances()
        {
            return AttendanceDAO.GetAttendances();
        }

        public void SaveAttendance(Attendance Attendance)
        {
            AttendanceDAO.SaveAttendance(Attendance);
        }

        public void UpdateAttendance(Attendance Attendance)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatusAttendance(int id, EnumList.AttendanceStatus attendanceStatus)
        {
            throw new NotImplementedException();
        }
    }
}
