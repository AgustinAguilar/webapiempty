using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Model
{
    public partial class DomainContext { public DbSet<Access> Accesses { get; set; } }

    public class AccessFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>(ent =>
            {
                ent.HasKey(p => p.Id);               
            });
        }
    }
}
