using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Material
{
    public int Idmaterial { get; set; }

    public string MaterialName { get; set; } = null!;

    public byte[] MaterialData { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public int? CourseId { get; set; }

    public int? UploadedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? UploadedByNavigation { get; set; }
}
