using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<Permission> Permissions { get; set; } }

    public class PermissionFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(ent =>
            {
                ent.HasKey(p => p.Id);
                
            });
        }
    }
}
