using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<Option> Options { get; set; } }

    public class OptionFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(ent =>
            {
                ent.HasKey(p => p.Id);
                ent.Property(p => p.Name)
                  .HasMaxLength(100)
                  .IsRequired();
                ent.Property(p => p.Description)
                   .HasMaxLength(100);
                ent.Property(p => p.Url)
                    .HasMaxLength(200);
                ent.Property(p => p.Icon)
                   .HasMaxLength(100);
            });
        }
    }
}
