using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Institution_net6.Models;

// tracks a particular class (consists of multiple class-days)
// that a student can enroll to. each class, even if it is for the 
// same module, can have different fee.
public class Class
{
  public Class()
  {
    Id = new Guid();
    ClassDays = new List<ClassDay>();
    Students = new List<Student>();
  }

  public Guid Id { get; set; }

  [Required]
  [MaxLength(6)]
  public string RefCode { get; set; }

  public float Fee { get; set; }

  public virtual Guid ModuleId { get; set; }

  // a Class consists of one or more Class Days
  public virtual ICollection<ClassDay> ClassDays { get; set; }

  // a Class can be enrolled by one or more Students
  public virtual ICollection<Student> Students { get; set; }
}

