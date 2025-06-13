using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {

        [HttpGet("sync")]

        public IActionResult GetSync()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            Thread.Sleep(1000); // Simulate a synchronous delay

            Console.WriteLine("Conexion a la base de datos terminada");

            Thread.Sleep(1000); // Simulate another synchronous delay

            Console.WriteLine("Envío de mail terminado");

            Console.WriteLine("Todo ha terminado");

            sw.Stop();
            return Ok(sw.Elapsed);
        }


        [HttpGet("async")]

        public async Task<IActionResult> GetAsync()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            var Task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000); // Simulate a synchronous delay

                Console.WriteLine("Conexion a la base de datos terminada");

                return 1;
            });

            var Task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000); // Simulate a synchronous delay

                Console.WriteLine("Envío de mail terminado");

                return 2;
            });

            Task1.Start();
            Task2.Start();

            Console.WriteLine("Hago otra cosa");

            var result1 = await Task1;
            var result2 = await Task2;

            Console.WriteLine("Todo ha terminado");

            sw.Stop();

            return Ok($"{result1} {result2} {sw.Elapsed}");
        }



    }
}
