using BackendExample.DTOs;
using FluentValidation;

namespace BackendExample.Validators
{
	public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
	{
		public BeerUpdateValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
			RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("BrandId must be greater than 0.");
			RuleFor(x => x.Alcohol).InclusiveBetween(0, 100).WithMessage("Alcohol content must be between 0 and 100%.");
		}
	}
}
