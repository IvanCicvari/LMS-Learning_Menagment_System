using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Grade
{
    public int Idgrade { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public int? ExamId { get; set; }

    public int Grade1 { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual User? Student { get; set; }
}
