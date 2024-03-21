using BusinessObject;
using DataTransfer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get() => Ok(attendanceRepository.GetAttendances());

        [HttpGet("{id}")]
        public ActionResult<Attendance> Get(int id)
        {
            var item = attendanceRepository.FindAttendanceById(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost("AttendanceEmployee")]
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
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
