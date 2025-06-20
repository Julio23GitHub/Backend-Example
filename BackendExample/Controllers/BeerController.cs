using BackendExample.DTOs;
using BackendExample.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BeerController : ControllerBase
	{

		private readonly StoreContext _context;
		private readonly IValidator<BeerInsertDto> _beerInsertValidator;
		private readonly IValidator<BeerUpdateDto> _beerUpdateValidator;

		public BeerController(StoreContext context, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator)
		{
			_context = context;
			_beerInsertValidator = beerInsertValidator;
			_beerUpdateValidator = beerUpdateValidator;
		}



		[HttpGet]
		public async Task<IEnumerable<BeerDto>> Get() =>
			await _context.Beers
				.Select(beer => new BeerDto
				{
					Id = beer.BeerId,
					Name = beer.Name,
					BrandId = beer.BrandID,
					Alcohol = beer.Alcohol
				}).ToListAsync();

		[HttpGet("{id}")]
		public async Task<ActionResult<BeerDto>> GetById(int id)
		{
			var beer = await _context.Beers.FindAsync(id);

			if (beer == null)
			{
				return NotFound();
			}


			var beerDto = new BeerDto
			{
				Id = beer.BeerId,
				Name = beer.Name,
				Alcohol = beer.Alcohol,
				BrandId = beer.BrandID
			};

			return Ok(beerDto);
		}


		[HttpPost]
		public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
		{
			var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}


			var beer = new Beer()
			{
				Name = beerInsertDto.Name,
				BrandID = beerInsertDto.BrandId,
				Alcohol = beerInsertDto.Alcohol,

			};

			await _context.Beers.AddAsync(beer);
			await _context.SaveChangesAsync();

			var beerDto = new BeerDto
			{
				Id = beer.BeerId,
				Name = beer.Name,
				Alcohol = beer.Alcohol,
				BrandId = beer.BrandID

			};

			return CreatedAtAction(nameof(GetById), new { id = beer.BeerId }, beerDto);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
		{

			var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			var beer = await _context.Beers.FindAsync(id);

			if (beer == null)
			{
				return NotFound();
			}

			beer.Name = beerUpdateDto.Name;
			beer.Alcohol = beerUpdateDto.Alcohol;
			beer.BrandID = beerUpdateDto.BrandId;
			await _context.SaveChangesAsync();

			var beerDto = new BeerDto
			{
				Id = beer.BeerId,
				Name = beer.Name,
				Alcohol = beer.Alcohol,
				BrandId = beer.BrandID
			};

			return Ok(beerDto);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var beer = await _context.Beers.FindAsync(id);

			if (beer == null)
			{
				return NotFound();
			}

			_context.Beers.Remove(beer);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
