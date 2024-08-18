using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Course
{
    public int Idcourse { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
