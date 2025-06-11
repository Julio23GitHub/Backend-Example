using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeoplesController : ControllerBase
	{
		private const string Id = "{id}";

		[HttpGet("all")]
		public ActionResult<List<People>> GetAll()
		{
			return Ok(Repository.PeopleList);
		}

		[HttpGet(Id)]
		public ActionResult<People> Get(int id)
		{
			var people = Repository.PeopleList.FirstOrDefault(p => p.Id == id);

			if (people == null)
			{
				return NotFound();
			}
			return Ok(people);
		}

		[HttpGet("search/{Search}")]
		public List<People> Get(string Search) => Repository.PeopleList.Where(p => p.Name.ToUpper().Contains(Search.ToUpper())).ToList();

		[HttpPost]
		public IActionResult Add(People people)
		{
			if (string.IsNullOrEmpty(people.Name))
			{
				return BadRequest();
			}

			Repository.PeopleList.Add(people);

			return NoContent();
		}
	}

	public class Repository
	{
		public static List<People> PeopleList = new List<People>
		{
			new People { Id = 1, Name = "John Doe", Birthdate = new DateTime(1990, 1, 1) },
			new People { Id = 2, Name = "Jane Smith", Birthdate = new DateTime(1985, 5, 15) },
			new People { Id = 3, Name = "Alice Johnson", Birthdate = new DateTime(2000, 12, 31) }
		};
	}

	public class People
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Birthdate { get; set; }

	}
}
