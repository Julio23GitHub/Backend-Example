using BackendExample.Controllers;

namespace BackendExample.Services
{
	public class People2Service : IPeopleService
	{
		public bool Validate(People people)
		{
			if (string.IsNullOrEmpty(people.Name))
			{
				return false;
			}
			return true;
		}
	}
}
