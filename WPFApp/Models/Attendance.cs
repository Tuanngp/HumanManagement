using System;
using System.Collections.Generic;

namespace WPFApp.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public int? OvertimeHours { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
