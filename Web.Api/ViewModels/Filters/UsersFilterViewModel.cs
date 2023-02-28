using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.ViewModels.Filters
{
    public class UsersFilterViewModel
    {
        public int Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}
