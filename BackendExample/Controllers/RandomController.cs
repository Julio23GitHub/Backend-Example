using BackendExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomServiceSingleton;
        private IRandomService _randomServiceScoped;
        private IRandomService _randomServiceTransient;


        private IRandomService _randomService2Singleton;
        private IRandomService _randomService2Scoped;
        private IRandomService _randomService2Transient;



        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("randomSingleton")] IRandomService randomService2Singleton,
            [FromKeyedServices("randomScoped")] IRandomService randomService2Scoped,
            [FromKeyedServices("randomTransient")] IRandomService randomService2Transient)
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;
            _randomService2Singleton = randomService2Singleton;
            _randomService2Scoped = randomService2Scoped;
            _randomService2Transient = randomService2Transient;

        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("randomSingleton1", _randomServiceSingleton.Value);
            result.Add("randomScoped1", _randomServiceScoped.Value);
            result.Add("randomTransient1", _randomServiceTransient.Value);
            result.Add("randomSingleton2", _randomService2Singleton.Value);
            result.Add("randomScoped2", _randomService2Scoped.Value);
            result.Add("randomTransient2", _randomService2Transient.Value);

            return result;

        }


    }
}
