using FluentValidation;

namespace WebApplication1.Validators
{
    public class CustomerValidator : AbstractValidator<WebApplication1.Models.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(c => !string.IsNullOrEmpty(c.Phone));
        }
    }
}