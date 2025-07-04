using AutoMapper;
using AutoMapper;
using BackendExample.AutoMappers;
using BackendExample.DTOs;
using BackendExample.Models;
using BackendExample.Repository;
using BackendExample.Services;
using BackendExample.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("People2Service");


builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");


builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");

//Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<StoreContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("StoreConnection")));


//Validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

//Repository
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();


//Mappers
var mapperConfig = new MapperConfiguration(cfg =>
{
	cfg.AddProfile<MappingProfile>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
	c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
