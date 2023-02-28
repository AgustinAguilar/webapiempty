using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<AccessType> AccessTypes { get; set; } }

    public class AccessTypeFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessType>(ent =>
            {
                ent.HasKey(p => p.Id);
                ent.Property(p => p.Name)
                 .HasMaxLength(50);
                ent.Property(p => p.Description)
                 .HasMaxLength(100);
            });
        }
    }
}
