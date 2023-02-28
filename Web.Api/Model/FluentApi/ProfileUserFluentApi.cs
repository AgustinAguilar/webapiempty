using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<ProfileUser> ProfileUsers { get; set; } }
    public class ProfileUserFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileUser>(ent =>
            {
                ent.HasKey(p => new { p.ProfileId, p.UserId });
            });
        }
    }
}
