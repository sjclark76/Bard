using System.Linq;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Testing.Sample.Api.Controllers
{
    [ApiController]
    [Route("api/bankaccounts")]
    public class BankAccountController : ControllerBase
    {
        private readonly BankDbContext _bankDbContext;

        public BankAccountController(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        [HttpGet("{id}")]
        public ActionResult<BankAccount> Get([FromRoute] int id)
        {
            var bankAccount = _bankDbContext.BankAccounts.SingleOrDefault(ba => ba.Id == id);

            if (bankAccount == null)
                return NotFound();

            return Ok(bankAccount);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var bankAccount = _bankDbContext.BankAccounts.SingleOrDefault(ba => ba.Id == id);

            if (bankAccount == null)
                return NotFound();

            bankAccount.IsActive = false;

            _bankDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult Create(BankAccount bankAccount)
        {
            _bankDbContext.BankAccounts.Add(bankAccount);

            _bankDbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new {id = bankAccount.Id}, bankAccount);
        }

        [HttpPost("{id}/deposits")]
        public ActionResult DepositMoney([FromRoute] int id, [FromBody] Deposit deposit)
        {
            var bankAccount = _bankDbContext.BankAccounts.SingleOrDefault(ba => ba.Id == id);

            if (bankAccount == null)
                return NotFound();

            bankAccount.DepositFunds(deposit);

            _bankDbContext.SaveChanges();

            return Ok(bankAccount);
        }

        [HttpPost("{id}/withdrawals")]
        public ActionResult WithdrawMoney([FromRoute] int id, [FromBody] Withdrawal withdrawal)
        {
            var bankAccount = _bankDbContext.BankAccounts.SingleOrDefault(ba => ba.Id == id);

            if (bankAccount == null)
                return NotFound();

            if (bankAccount.CanMake(withdrawal))
            {
                bankAccount.WithdrawFunds(withdrawal);
                _bankDbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("Insufficient Funds to make withdrawal.");
        }
    }
}