using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Institution_net6.Models;

// tracks a lecturer and his teach-days
public class Lecturer
{
  public Lecturer()
  {
    Id = new Guid();
    ClassDays = new List<ClassDay>();
  }

  public Guid Id { get; set; }

  [Required]
  [MaxLength(36)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(36)]
  public string LastName { get; set; }

  // one lecturer can teach multiple Class Days
  public virtual ICollection<ClassDay> ClassDays { get; set; }
}

