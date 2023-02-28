using Web.Api.Model.Classes.BaseEntities;
using System.Collections.Generic;

namespace Web.Api.Model.Classes
{
    public partial class Permission : BaseIdentificableEntity
    {
        public int? OptionId { get; set; }
        public Option Option { get; set; }
        public int FunctionId { get; set; }
        public Function Function { get; set; }
        public virtual ICollection<PermissionUser> PermissionUsers { get; set; }
        public virtual ICollection<ProfilePermission> ProfilePermissions { get; set; }
    }
}
