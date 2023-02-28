using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Web.Api.Model
{
    public abstract class BaseFluentApi
    {
        public abstract void OnClassCreating(ModelBuilder modelBuilder);

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(p=> p.BaseType == typeof(BaseFluentApi))
                .ToList()
                .ForEach(p=> ((BaseFluentApi)Activator.CreateInstance(p)).OnClassCreating(modelBuilder));
        }
    }
}
