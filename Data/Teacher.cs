using System;
using System.Collections.Generic;

namespace TeachersMVC.Data;

public partial class Teacher
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
