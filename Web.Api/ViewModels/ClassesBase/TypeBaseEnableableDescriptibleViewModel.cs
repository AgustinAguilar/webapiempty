using System;

namespace Web.Api.ViewModels.ClassesBase
{
    public class TypeBaseEnableableDescriptibleViewModel : TypeBaseEnableableViewModel
    {
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TypeBaseEnableableDescriptibleViewModel model &&
                   base.Equals(obj) &&
                   Description == model.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Description);
        }
    }
}
