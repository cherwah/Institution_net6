using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Institution_net6.Models;

namespace Institution_net6.DB;

public class DBManager
{
  private DBContext dbContext;

  public DBManager(DBContext dbContext)
  {
    this.dbContext = dbContext;
  }

  public void Seed()
  {
    SeedModules();
    SeedLecturers();
    SeedStudents();
    SeedClasses();
    SeedClassDays();
    SeedEnrollment();
  }

  public void ListTeachDays(string FirstName, string LastName)
  {
    // list dates that the lecturer is teaching
    Lecturer? lecturer = dbContext.Lecturers.FirstOrDefault(x =>
        x.FirstName == FirstName && x.LastName == LastName
    );

    if (lecturer != null)
    {
      Debug.WriteLine("\nListTeachDays: ");

      List<ClassDay> classdays = (List<ClassDay>)lecturer.ClassDays;
      foreach (ClassDay classday in classdays)
      {
        DateTime datetime = classday.RunDate;
        
        Debug.WriteLine("{0} {1} {2}",
            datetime.Day, datetime.Month, datetime.Year);
      }
    }
  }

  public void ListModulesWithNoClasses()
  {
    // list modules that currently have no class days
    List<Module> modules = dbContext.Modules.Where(x =>
        x.Classes.Count() == 0
    ).ToList();

    Debug.WriteLine("\nListModulesWithNoClasses: ");
    foreach (Module module in modules)
    {
      Debug.WriteLine("{0} - {1}", module.RefCode, module.Title);
    }
  }

  public void ListStudentsWithAtLeastOneClass()
  {
    // list the students that have enrolled to at least one class
    List<Student> students = dbContext.Students.Where(x =>
        x.Classes.Count() >= 1
    ).ToList();

    Debug.WriteLine("\nListStudentsWithAtLeastOneClass: ");
    foreach (Student student in students)
    {
      Debug.WriteLine("{0} {1}", student.FirstName, student.LastName);
    }
  }

  public void UpdateClassFee(int ndays, float fee)
  {
    // update classes with ndays to new fee
    List<Class> classes = dbContext.Classes.Where(x =>
        x.ClassDays.Count() == 2
    ).ToList();

    foreach (Class aclass in classes)
    {
      aclass.Fee = fee;
    }

    dbContext.SaveChanges();
  }

  private void SeedModules()
  {
    dbContext.Add(new Module
    {
      RefCode = "NETC",
      Title = "NET Core",
      Desc = "Learn Web Development with .NET Core",
    });

    dbContext.Add(new Module
    {
      RefCode = "MLPY",
      Title = "Machine Learning in Python",
      Desc = "Learn ML Techniques with Python"
    });

    dbContext.SaveChanges();
  }

  private void SeedLecturers()
  {
    dbContext.Add(new Lecturer
    {
      FirstName = "Kim",
      LastName = "Tan"
    });

    dbContext.Add(new Lecturer
    {
      FirstName = "Lynn",
      LastName = "Wong"
    });

    dbContext.SaveChanges();
  }

  private void SeedStudents()
  {
    dbContext.Add(new Student
    {
      FirstName = "Jean",
      LastName = "Sim",
    });

    dbContext.Add(new Student
    {
      FirstName = "Kate",
      LastName = "Lim"
    });

    dbContext.Add(new Student
    {
      FirstName = "Lynn",
      LastName = "Ho"
    });

    dbContext.Add(new Student
    {
      FirstName = "James",
      LastName = "See"
    });

    dbContext.SaveChanges();
  }

  private void SeedClasses()
  {
    Module? module = dbContext.Modules.FirstOrDefault(x =>
        x.RefCode == "NETC"
    );

    if (module != null)
    {
      module.Classes.Add(new Class
      {
        RefCode = "ISS001",
        Fee = 300,
      });

      module.Classes.Add(new Class
      {
        RefCode = "ISS002",
        Fee = 600,
      });

      dbContext.SaveChanges();
    }
  }

  private void SeedClassDays()
  {
    Lecturer? lecturer1 = dbContext.Lecturers.FirstOrDefault(x =>
        x.FirstName == "Kim" && x.LastName == "Tan"
    );
    Lecturer? lecturer2 = dbContext.Lecturers.FirstOrDefault(x =>
        x.FirstName == "Lynn" && x.LastName == "Wong"
    );

    /* create a class with a single class-day */
    Class? aclass = dbContext.Classes.FirstOrDefault(x =>
        x.RefCode == "ISS001"
    );

    if (aclass != null)
    {
      ClassDay day1 = new ClassDay();
      day1.RunDate = new DateTime(2021, 11, 1, 9, 0, 0, DateTimeKind.Local);
      day1.Class = aclass;
      day1.Lecturer = lecturer1;

      dbContext.Add(day1);
    }

    /* create another class with two class-days */
    Class? aclass2 = dbContext.Classes.FirstOrDefault(x =>
        x.RefCode == "ISS002"
    );

    if (aclass2 != null)
    {
      ClassDay day1 = new ClassDay();
      day1.RunDate = new DateTime(2021, 12, 1, 9, 0, 0, DateTimeKind.Local);
      day1.Class = aclass2;
      day1.Lecturer = lecturer1;

      ClassDay day2 = new ClassDay();
      day2.RunDate = new DateTime(2021, 12, 2, 9, 0, 0, DateTimeKind.Local);
      day2.Class = aclass2;
      day2.Lecturer = lecturer2;

      dbContext.Add(day1);
      dbContext.Add(day2);
    }
    
    dbContext.SaveChanges();
  }

  private void SeedEnrollment()
  {
    Class? aclass = dbContext.Classes.FirstOrDefault(x =>
        x.RefCode == "ISS001"
    );

    if (aclass != null)
    {
      Student? student1 = dbContext.Students.FirstOrDefault(x =>
          x.FirstName == "James" && x.LastName == "See"
      );
      Student? student2 = dbContext.Students.FirstOrDefault(x =>
          x.FirstName == "Jean" && x.LastName == "Sim"
      );

      aclass.Students.Add(student1);
      aclass.Students.Add(student2);
    }

    dbContext.SaveChanges();
  }
}

