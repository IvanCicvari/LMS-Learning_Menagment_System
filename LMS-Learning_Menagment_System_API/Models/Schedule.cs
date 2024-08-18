using System;
using System.Collections.Generic;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class Schedule
{
    public int Idschedule { get; set; }

    public int? CourseId { get; set; }

    public int? ExamId { get; set; }

    public DateOnly ScheduleDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Exam? Exam { get; set; }
}
