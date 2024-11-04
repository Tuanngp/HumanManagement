using System;
using System.Collections.Generic;

namespace WPFApp.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int EmployeeId { get; set; }

    public double BaseSalary { get; set; }

    public double? Allowance { get; set; }

    public double? Bonus { get; set; }

    public double? Penalty { get; set; }

    public double? TotalIncome { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
