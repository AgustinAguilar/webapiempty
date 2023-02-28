using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<RefreshToken> RefreshTokens { get; set; } }

    public class RefreshTokenFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(ent =>
            {
                ent.HasKey(p => p.Id);
                ent.Property(p => p.Token)
                    .IsRequired()
                    .HasMaxLength(200);
                ent.Property(p => p.CreatedByIp)
                    .IsRequired()
                    .HasMaxLength(30);
                ent.Property(p => p.RevokedByIp)
                   .HasMaxLength(30);
            });
        }
    }
}
