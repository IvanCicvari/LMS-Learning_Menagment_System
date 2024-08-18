using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Homework
{
    public int Idhomework { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly DueDate { get; set; }

    public int? CourseId { get; set; }

    public int? AssignedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User? AssignedByNavigation { get; set; }

    public virtual Course? Course { get; set; }
}
