﻿using BackendExample.DTOs;
using BackendExample.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BeerController : ControllerBase
	{


		private readonly IValidator<BeerInsertDto> _beerInsertValidator;
		private readonly IValidator<BeerUpdateDto> _beerUpdateValidator;
		private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

		public BeerController(IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator,
			[FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
		{
			_beerInsertValidator = beerInsertValidator;
			_beerUpdateValidator = beerUpdateValidator;
			_beerService = beerService;
		}



		[HttpGet]
		public async Task<IEnumerable<BeerDto>> Get() =>
			await _beerService.Get();

		[HttpGet("{id}")]
		public async Task<ActionResult<BeerDto>> GetById(int id)
		{
			var beerDto = await _beerService.GetById(id);

			return beerDto == null ? NotFound() : Ok(beerDto);
		}


		[HttpPost]
		public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
		{
			var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			if (!_beerService.Validate(beerInsertDto))
			{
				return BadRequest(_beerService.Errors);
			}

			var beerDto = await _beerService.Add(beerInsertDto);



			return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
		{

			var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			if (!_beerService.Validate(beerUpdateDto))
			{
				return BadRequest(_beerService.Errors);
			}

			var beerDto = await _beerService.Update(id, beerUpdateDto);


			return beerDto == null ? NotFound() : Ok(beerDto);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<BeerDto>> Delete(int id)
		{


			var beerDto = await _beerService.Delete(id);

			return beerDto == null ? NotFound() : Ok(beerDto);
		}
	}
}
