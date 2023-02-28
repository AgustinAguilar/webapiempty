using System;

namespace Web.Api.ViewModels.ClassesBase
{
    public class TypeBaseEnableableViewModel : TypeBaseViewModel
    {
        public bool Enabled { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TypeBaseEnableableViewModel model &&
                   base.Equals(obj) &&
                   Enabled == model.Enabled;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Enabled);
        }
    }
}
