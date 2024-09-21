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
  public DbSet<City> Cityes { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Status> Statuses { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Stock> Stocks { get; set; }
  public DbSet<Invoice> Invoices { get; set; }
  public DbSet<Cargo> Cargoes { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Stock>().HasOne(p => p.City).WithMany(t => t.Stocks).HasForeignKey(t => t.CityId);
    modelBuilder.Entity<User>().HasOne(p => p.Role).WithMany(t => t.Users).HasForeignKey(t => t.RoleId);
    modelBuilder.Entity<User>().HasMany(t => t.SendInvoices).WithOne(t => t.Sender).HasForeignKey(t => t.SenderId);
    modelBuilder.Entity<User>().HasMany(t => t.ReceiveInvoices).WithOne(t => t.Recipient).HasForeignKey(t => t.RecipientId);
    modelBuilder.Entity<Invoice>().HasOne(t => t.Status).WithMany(t => t.Invoices).HasForeignKey(t => t.StatusId);
    modelBuilder.Entity<Invoice>().HasOne(t => t.Endpoint).WithMany(t => t.Invoices).HasForeignKey(t => t.EndpointId);
    modelBuilder.Entity<Invoice>().HasMany(t => t.Cargoes).WithOne(t => t.Invoice).HasForeignKey(t => t.InvoiceId);
  }
}
