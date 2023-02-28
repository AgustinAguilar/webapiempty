using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<Profile> Profiles { get; set; } }
    public class ProfileFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(ent =>
            {
                ent.HasKey(p => p.Id);
                ent.Property(p => p.Name)
                  .HasMaxLength(100)
                  .IsRequired();
                ent.Property(p => p.Description)
                   .HasMaxLength(100);
            });
        }
    }
}
