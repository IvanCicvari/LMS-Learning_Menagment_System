using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class City
{
    public int Idcity { get; set; }

    public string Name { get; set; } = null!;

    public int? CountryId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
