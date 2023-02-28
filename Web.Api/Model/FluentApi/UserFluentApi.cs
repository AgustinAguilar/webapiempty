using Web.Api.Model.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model
{

    public partial class DomainContext { public DbSet<User> Users { get; set; }}
    public class UserFluentApi : BaseFluentApi
    {
        public override void OnClassCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasKey(p => p.Id);
                ent.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                ent.Property(p => p.Password)
                    .IsRequired()
                    .HasMaxLength(100);
                ent.Property(p => p.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();
                ent.Property(p => p.LastName)
                   .HasMaxLength(100)
                   .IsRequired();
            });
        }
    }
}
