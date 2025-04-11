using Microsoft.EntityFrameworkCore;
using Avatar.Api.Repository.Entities;
namespace Avatar.Api.Repository;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    #region data sets declaration
    public DbSet<Skill> Skills { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamMemberSkill> TeamMemberSkills { get; set; }    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentDate = DateTime.Now;
        var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

        foreach (var entity in addedEntities)
        {
            if (entity.Properties.Any(p => p.Metadata.Name == "CreatedOn"))
            {
                entity.Property("CreatedOn").CurrentValue = currentDate;
            }
        }

        var updatedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
        foreach (var entity in updatedEntities)
        {
            if (entity.Properties.Any(p => p.Metadata.Name == "ChangedOn"))
            {
                entity.Property("ChangedOn").CurrentValue = currentDate;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}