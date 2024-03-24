namespace clients.Models
{
    public class AttendanceEmployee
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Hour { get; set; }
        public double OTHour { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
    }

    public class AddAttendanceEmployee
    {
        public DateTime Date { get; set; }
        public double Hour { get; set; }
        public double OTHour { get; set; }
        public int Type { get; set; }
    }

    public class AdminAttendance
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public double Hour { get; set; }
        public double OTHour { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
    }
}
