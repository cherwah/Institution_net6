using System;
namespace Institution_net6.Models;

// tracks a particular day for a class
public class ClassDay
{
    public ClassDay()
    {
        Id = new Guid();            
    }

    public Guid Id { get; set; }        
    public DateTime RunDate { get; set; }

    // a Class Day is associated with only one Class
    public virtual Class Class { get; set; }

    // a Class Day is taught by only one Lecturer
    public virtual Lecturer Lecturer { get; set; } 
}

