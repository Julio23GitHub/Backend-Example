using AutoMapper;
using BackendExample.DTOs;
using BackendExample.Models;
using BackendExample.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendExample.Services
{
	public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
	{

		private readonly IRepository<Beer> _beerRepository;

		private readonly IMapper _mapper;

		public List<string> Errors { get; }
		public BeerService(IRepository<Beer> beerRepository, IMapper mapper)
		{
			_beerRepository = beerRepository;
			_mapper = mapper;
			Errors = new List<string>();
		}
		public async Task<IEnumerable<BeerDto>> Get()
		{
			var beers = await _beerRepository.Get();

			return beers
				.Select(beer => _mapper.Map<BeerDto>(beer));
		}
		public async Task<BeerDto> GetById(int id)
		{
			var beer = await _beerRepository.GetById(id);

			if (beer != null)
			{
				var beerDto = _mapper.Map<BeerDto>(beer);

				return beerDto;
			}

			return null;
		}
		public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
		{
			var beer = _mapper.Map<Beer>(beerInsertDto);

			await _beerRepository.Add(beer);
			await _beerRepository.Save();


			var beerDto = _mapper.Map<BeerDto>(beer);


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

				var beerDto = _mapper.Map<BeerDto>(beer);
				return beerDto;
			}

			return null;
		}

		public async Task<BeerDto> Delete(int id)
		{
			var beer = await _beerRepository.GetById(id); ;

			if (beer != null)
			{
				var beerDto = _mapper.Map<BeerDto>(beer);
				_beerRepository.Delete(beer);
				await _beerRepository.Save();


				return beerDto;
			}

			return null;
		}

		public bool Validate(BeerInsertDto beerInsertDto)
		{
			if (_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count() > 0)
			{
				Errors.Add("No puede existir una cerveza con un nombre ya existente");
				return false;
			}
			return true;
		}

		public bool Validate(BeerUpdateDto beerUpdateDto)
		{

			if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name && beerUpdateDto.Id != b.BeerId).Count() > 0)
			{
				Errors.Add("No puede existir una cerveza con un nombre ya existente");
				return false;
			}
			return true;
		}
	}
}
