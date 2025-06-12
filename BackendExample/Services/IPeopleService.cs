using BackendExample.Controllers;

namespace BackendExample.Services
{
	public interface IPeopleService
	{
		bool Validate(People people);
	}
}
