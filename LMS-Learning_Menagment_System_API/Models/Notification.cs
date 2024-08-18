using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Notification
{
    public int Idnotification { get; set; }

    public int? UserId { get; set; }

    public int? NotificationTypeId { get; set; }

    public string NotificationText { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual NotificationType? NotificationType { get; set; }

    public virtual User? User { get; set; }
}
