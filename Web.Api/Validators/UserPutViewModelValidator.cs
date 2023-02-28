using Web.Api.ViewModels;
using FluentValidation;

namespace Web.Api.Controllers.Validators
{
    public class UserPutViewModelValidator : AbstractValidator<UserPutViewModel>
    {
        public UserPutViewModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(0, 100);
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(0, 100);
            RuleFor(x => x.Email).NotNull().NotEmpty().Length(0, 100);
        }
    }


}
