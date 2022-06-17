using System;
using Microsoft.EntityFrameworkCore;
using Institution_net6.Models;

namespace Institution_net6.DB;

// delete the created database if you updated anything
// in the DBContext class

public class DBContext : DbContext
{
  public DBContext(DbContextOptions<DBContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder model)
  {

  }

  // maps to our tables in the database
  public DbSet<Class> Classes { get; set; }
  public DbSet<ClassDay> ClassDays { get; set; }
  public DbSet<Lecturer> Lecturers { get;  set; }
  public DbSet<Student> Students { get; set; }
  public DbSet<Module> Modules { get; set; }  
}

