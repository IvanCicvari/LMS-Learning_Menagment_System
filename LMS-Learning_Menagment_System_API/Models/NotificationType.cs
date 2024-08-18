using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class NotificationType
{
    public int IdnotificationType { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
