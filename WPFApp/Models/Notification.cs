using System;
using System.Collections.Generic;

namespace WPFApp.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int DepartmentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Department Department { get; set; } = null!;
}
