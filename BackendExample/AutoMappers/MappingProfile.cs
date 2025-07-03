using AutoMapper;
using BackendExample.DTOs;
using BackendExample.Models;

namespace BackendExample.AutoMappers
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			CreateMap<BeerInsertDto, Beer>();
			CreateMap<Beer, BeerDto>()
				.ForMember(dto => dto.Id, m => m.MapFrom(b => b.BrandID));
		}
	}
}
