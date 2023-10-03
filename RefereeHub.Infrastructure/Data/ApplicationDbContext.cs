using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RefereeHub.Domain.Events;
using RefereeHub.Domain.Referee;
using RefereeHub.Domain.Report;

namespace RefereeHub.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .HasOne(o => o.Report)
            .WithMany(c => c.Events)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(o => o.ReportId);
    }

    public DbSet<Report> Reports { get; set; }
    public DbSet<Referee> Referees { get; set; }
    public DbSet<Event> Events { get; set; }
}