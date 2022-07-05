using FluentValidation;
using Unosquare.ToysAndGames.Models.Dtos;

namespace Unosquare.ToysAndGames.Models.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotNull().NotEmpty();
            RuleFor(product => product.Description).NotNull().NotEmpty();
            RuleFor(product => product.AgeRestriction).NotNull().InclusiveBetween(0,100);
            RuleFor(product => product.CompanyId).NotNull();
            RuleFor(product => product.Price).InclusiveBetween(1,1000);
        }
    }
}
