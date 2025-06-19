using BackendExample.DTOs;
using FluentValidation;

namespace BackendExample.Validators
{
	public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
	{
		public BeerInsertValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
				.MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
			RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("BrandId must be greater than 0.");
			RuleFor(x => x.Alcohol).InclusiveBetween(0, 100).WithMessage("Alcohol content must be between 0 and 100%.");
		}
	}
}
