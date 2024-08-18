using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Exam
{
    public int Idexam { get; set; }

    public string ExamName { get; set; } = null!;

    public int? CourseId { get; set; }

    public DateOnly ExamDate { get; set; }

    public int? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
