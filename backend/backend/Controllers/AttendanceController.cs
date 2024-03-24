using BusinessObject;
using BusinessObject.Enum;
using DataTransfer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Helper;
using Repositories.Impl;
using System.Globalization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository attendanceRepository = new AttendaceReposiory();

        [HttpGet]
        public IActionResult Get()
        {
            var check = attendanceRepository.GetAttendances();
            List<AdminAttendance> admins = new List<AdminAttendance>();
            foreach (var item in check)
            {

                AdminAttendance a = new AdminAttendance
                {
                    Id = item.Id,
                    Date = item.Date,
                    EmployeeId = item.EmployeeId,
                    Hour = item.Hour,
                    OTHour = item.OTHour,
                    Type = item.Type,
                    Status = UserHelper.GetAttandanceStatusString(item.Status),
                    EmployeeName = item.User.EmployeeName + " - " + item.User.EmployeeCode
                };
                admins.Add(a);
            }
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public ActionResult<Attendance> Get(int id)
        {
            var item = attendanceRepository.FindAttendanceById(id);

            if (item == null) return NotFound();

            return Ok(item);
        }


        [HttpGet("AttendanceEmployee/{id}")]
        [Authorize(Roles = "Employee")]
        public IActionResult GetAttendanceByEmpId(int id)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Invalid token");
                if (token.StartsWith("Bearer "))
                    token = token.Substring("Bearer ".Length).Trim();
                var employeeId = UserHelper.GetEmployeeIdFromToken(token);

                var attendanceEmp = attendanceRepository.FindAttendanceByEmpId(id);
                if (attendanceEmp.Count == 0)
                {
                    return BadRequest("Employee need attendance today");
                }
                return Ok(attendanceEmp);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("AttendanceEmployee")]
        [Authorize(Roles = "Employee")]
        public IActionResult EmployeePost([FromBody] AttendanceEmployeeReq attendanceRq)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Invalid token");
                if (token.StartsWith("Bearer "))
                    token = token.Substring("Bearer ".Length).Trim();
                var employeeId = UserHelper.GetEmployeeIdFromToken(token);
                var tempAttendance = attendanceRepository.FindAttendanceByUserAndDay
                    (employeeId, attendanceRq.Date.Date);

                if (tempAttendance != null)
                    return BadRequest("Attendance already exists.");

                var current = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                    (attendanceRq.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

                var now = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                    (DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
                if (current < now && attendanceRq.Date.Year < DateTime.Now.Year)
                    return Conflict("Date time create is out!");


                Attendance newAttendance = new Attendance
                {
                    Date = attendanceRq.Date,
                    Hour = attendanceRq.Hour,
                    OTHour = attendanceRq.OTHour,
                    Type = attendanceRq.Type,
                    EmployeeId = employeeId
                };
                attendanceRepository.SaveAttendance(newAttendance);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var attendance = attendanceRepository.FindAttendanceById(id);
                if (attendance == null)
                    return BadRequest("id khong ton tai");
                attendanceRepository.DeleteAttendance(attendance);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{key}")]
        public IActionResult Patch(int key, EnumList.AttendanceStatus attendanceStatus)
        {
            var attendance = attendanceRepository.FindAttendanceById(key);

            if (attendance == null)
                return NotFound();
            try
            {
                attendanceRepository.UpdateStatusAttendance(key, attendanceStatus);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
