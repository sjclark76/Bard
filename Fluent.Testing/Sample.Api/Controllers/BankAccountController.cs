using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Testing.Sample.Api.Controllers
{
    [ApiController]
    [Route("api/bankaccount")]
    public class BankAccountController : ControllerBase
    {
        private readonly BankDbContext _bankDbContext;

        public BankAccountController(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> Get([FromRoute] int id)
        {
            var bankAccount = _bankDbContext.BankAccounts.SingleOrDefault(ba => ba.Id == id);

            if (bankAccount == null)
                return NotFound();

            return Ok(bankAccount);
        }
        
        [HttpPost]
        public ActionResult Create(BankAccount bankAccount)
        {
            _bankDbContext.BankAccounts.Add(bankAccount);

            _bankDbContext.SaveChanges();
            
            return CreatedAtAction(nameof(Get), new {id = bankAccount.Id}, bankAccount);
        }
    }
}