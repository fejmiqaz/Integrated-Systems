using CoursesApplication.Domain.ExternalModels;
using Microsoft.EntityFrameworkCore;

namespace CoursesApplication.Repository;

public class LegacyApplicationDbContext(DbContextOptions<LegacyApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LegacyAcademicPeriod> AcademicPeriods { get; set; }
    public DbSet<LegacySubjectCatalog> SubjectCatalogs { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyAcademicPeriod>(e => e.HasKey(v => v.PeriodId));
        modelBuilder.Entity<LegacySubjectCatalog>(e => e.HasKey(v => v.SubjectCode));
    }
}
