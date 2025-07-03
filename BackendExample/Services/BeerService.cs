using BackendExample.DTOs;
using BackendExample.Models;
using BackendExample.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendExample.Services
{
	public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
	{

		private readonly IRepository<Beer> _beerRepository;

		public BeerService(IRepository<Beer> beerRepository)
		{
			_beerRepository = beerRepository;
		}
		public async Task<IEnumerable<BeerDto>> Get()
		{
			var beers = await _beerRepository.Get();

			return beers
				.Select(beer => new BeerDto
				{
					Id = beer.BeerId,
					Name = beer.Name,
					BrandId = beer.BrandID,
					Alcohol = beer.Alcohol
				});
		}
		public async Task<BeerDto> GetById(int id)
		{
			var beer = await _beerRepository.GetById(id);

			if (beer != null)
			{
				var beerDto = new BeerDto
				{
					Id = beer.BeerId,
					Name = beer.Name,
					BrandId = beer.BrandID,
					Alcohol = beer.Alcohol
				};

				return beerDto;
			}

			return null;
		}
		public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
		{
			var beer = new Beer()
			{
				Name = beerInsertDto.Name,
				BrandID = beerInsertDto.BrandId,
				Alcohol = beerInsertDto.Alcohol,

			};

			await _beerRepository.Add(beer);
			await _beerRepository.Save();


			var beerDto = new BeerDto
			{
				Id = beer.BeerId,
				Name = beer.Name,
				Alcohol = beer.Alcohol,
				BrandId = beer.BrandID

			};

			return beerDto;
		}
		public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
		{
			var beer = await _beerRepository.GetById(id);

			if (beer != null)
			{
				beer.Name = beerUpdateDto.Name;
				beer.Alcohol = beerUpdateDto.Alcohol;
				beer.BrandID = beerUpdateDto.BrandId;

				_beerRepository.Update(beer);
				await _beerRepository.Save();

				var beerDto = new BeerDto
				{
					Id = beer.BeerId,
					Name = beer.Name,
					Alcohol = beer.Alcohol,
					BrandId = beer.BrandID
				};
				return beerDto;
			}

			return null;
		}

		public async Task<BeerDto> Delete(int id)
		{
			var beer = await _beerRepository.GetById(id); ;

			if (beer != null)
			{
				var beerDto = new BeerDto
				{
					Id = beer.BeerId,
					Name = beer.Name,
					Alcohol = beer.Alcohol,
					BrandId = beer.BrandID
				};
				_beerRepository.Delete(beer);
				await _beerRepository.Save();


				return beerDto;
			}

			return null;
		}

	}
}
