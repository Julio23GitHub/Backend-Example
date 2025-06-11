using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Operation : ControllerBase
	{
		// GET: api/<Operation>
		[HttpGet]
		public decimal Get(decimal a, decimal b)
		{
			return a + b;
		}


		// POST api/<Operation>
		[HttpPost]
		public decimal Post(Number number, [FromHeader] string Host, [FromHeader(Name = "Content-Length")] string ContentLength
			, [FromHeader(Name = "x-some")] string Some)
		{
			Console.WriteLine($"The host is {Host} {ContentLength}  {Some}");
			return number.a - number.b;
		}

		[HttpPut]
		public decimal Edit(Number number)
		{
			return number.a * number.b;
		}

		[HttpDelete]
		public decimal Delete(decimal a, decimal b)
		{
			return a / b;
		}

	}

	public class Number
	{
		public decimal a { get; set; }
		public decimal b { get; set; }
	}
}
