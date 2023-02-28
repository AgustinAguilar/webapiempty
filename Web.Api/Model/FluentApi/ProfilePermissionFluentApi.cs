using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<ProfilePermission> ProfilePermissions { get; set; } }
    public class ProfilePermissionFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfilePermission>(ent =>
            {
                ent.HasKey(p => new { p.ProfileId, p.PermissionId });
            });
        }
    }
}
