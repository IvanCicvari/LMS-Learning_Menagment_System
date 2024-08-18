using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Class
{
    public int Idclass { get; set; }

    public string ClassName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
