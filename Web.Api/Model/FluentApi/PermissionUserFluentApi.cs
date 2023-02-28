using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<PermissionUser> PermissionUsers { get; set; } }
    public class PermissionUserFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionUser>(ent =>
            {
                ent.HasKey(p => new { p.PermissionId, p.UserId });
            });
        }
    }
}
