using Microsoft.EntityFrameworkCore;
using Clinic.Models;

namespace Clinic;

public class ClinicDbContext : DbContext
{
  public string DbPath { get; }
  public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
  {
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    DbPath = System.IO.Path.Join(path, "SmallDb.db");
    Console.Write("DBPath: ");
    Console.WriteLine(DbPath);
  }

  // The following configures EF to create a Sqlite database file in the
  // special "local" folder for your platform.
  protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite($"Data Source={DbPath}");
  public DbSet<Profession> Professions { get; set; }
  public DbSet<Client> Clients { get; set; }
  public DbSet<Doctor> Doctors { get; set; }
  public DbSet<Disease> Diseases { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Diagnosis> Diagnosis { get; set; }
  public DbSet<Appointment> Appointments{ get; set; }
  public DbSet<CategoryDisease> CategoryDiseases { get; set; }
  public DbSet<Role> Roles { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasOne(p => p.Role).WithMany(t => t.Users).HasForeignKey(t => t.RoleId);
    modelBuilder.Entity<Profession>().HasMany(t => t.Doctors).WithOne(t => t.Profession).HasForeignKey(t => t.ProfessionId);
    modelBuilder.Entity<Disease>().HasOne(p => p.CategoryDisease).WithMany(t => t.Diseases).HasForeignKey(t => t.CategoryDiseaseId);
    modelBuilder.Entity<Disease>().HasMany(p => p.Diagnosis).WithMany(t => t.Diseases);
    modelBuilder.Entity<Diagnosis>().HasOne(p => p.Client).WithMany(t => t.Diagnosis).HasForeignKey(t => t.ClientId);
    modelBuilder.Entity<Diagnosis>().HasOne(p => p.Doctor).WithMany(t => t.Diagnosis).HasForeignKey(t => t.DoctorId);
    modelBuilder.Entity<Appointment>().HasOne(p => p.Client).WithMany(t => t.Appointments).HasForeignKey(t => t.ClientId);
    modelBuilder.Entity<Appointment>().HasOne(p => p.Doctor).WithMany(t => t.Appointments).HasForeignKey(t => t.DoctorId);
    modelBuilder.Entity<Diagnosis>().HasOne(p => p.Doctor).WithMany(t => t.Diagnosis).HasForeignKey(t => t.DoctorId);
  }
}
