using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Institution_net6.Models;

// tracks a student and all his registered classes
public class Student
{
  public Student()
  {
    Id = new Guid();
    Classes = new List<Class>();
  }

  public Guid Id { get; set; }

  [Required]
  [MaxLength(36)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(36)]
  public string LastName { get; set; }

  public virtual ICollection<Class> Classes { get; set; }
}

