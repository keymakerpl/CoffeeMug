using CoffeeMug.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeeMug.Controllers
{
    public class DummyController : Controller
    {
        private readonly CoffeeMugDbContext _context;
        private readonly ILogger<DummyController> _logger;

        public DummyController(CoffeeMugDbContext context, ILogger<DummyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/testdb")]
        public IActionResult TestDatabase()
        {
            if (_context == null) return StatusCode(500, "Db context failed");

            return Ok();
        }

        [HttpGet]
        [Route("api/testlog")]
        public IActionResult TestLog()
        {
            _logger.LogCritical("Has been logged");

            return StatusCode(500, "TestLog");            
        }
    }
}
