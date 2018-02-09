using FluentValidation;

namespace Domain
{
    public class AddAdminValidator : AbstractValidator<AddAdminUser>
    {
        public AddAdminValidator()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty()
                .WithMessage("First name is not correct - Has to be defined");

            RuleFor(_ => _.LastName)
                .NotEmpty()
                .WithMessage("Last name is not correct - Has to be defined");

            RuleFor(_ => _.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Email is invalid - Has to be of valid format");
        }
        
    }
}
