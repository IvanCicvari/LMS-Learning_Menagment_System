using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Message
{
    public int Idmessage { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
