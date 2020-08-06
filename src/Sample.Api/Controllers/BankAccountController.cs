using System.Threading;
using System.Threading.Tasks;
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
        public async Task<ActionResult<BankAccount>> Get([FromRoute] int id)
        {
            var bankAccount = await _bankDbContext.BankAccounts.FindAsync(id);

            if (bankAccount == null)
                return NotFound();

            return Ok(bankAccount);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BankAccount>> Update([FromRoute] int id, [FromBody] BankAccount update,
            CancellationToken cancellationToken = default)
        {
            var bankAccount = await _bankDbContext.BankAccounts.FindAsync(id);

            if (bankAccount == null)
                return NotFound();

            bankAccount.Balance = update.Balance;
            bankAccount.CustomerName = update.CustomerName;

            await _bankDbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var bankAccount = await _bankDbContext.BankAccounts.FindAsync(id);

            if (bankAccount == null)
                return NotFound();

            bankAccount.IsActive = false;

            await _bankDbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BankAccount bankAccount, CancellationToken cancellationToken = default)
        {
            await _bankDbContext.BankAccounts.AddAsync(bankAccount, cancellationToken);

            await _bankDbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(Get), new {id = bankAccount.Id}, bankAccount);
        }

        [HttpPost("{id}/deposits")]
        public async Task<ActionResult> DepositMoney([FromRoute] int id, [FromBody] Deposit deposit,
            CancellationToken cancellationToken = default)
        {
            var bankAccount = await _bankDbContext.BankAccounts.FindAsync(id);

            if (bankAccount == null)
                return NotFound();

            bankAccount.DepositFunds(deposit.Amount.GetValueOrDefault());

            await _bankDbContext.SaveChangesAsync(cancellationToken);

            return Ok(bankAccount);
        }

        [HttpPost("{id}/withdrawals")]
        public async Task<ActionResult> WithdrawMoney([FromRoute] int id, [FromBody] Withdrawal withdrawal,
            CancellationToken cancellationToken = default)
        {
            var bankAccount = await _bankDbContext.BankAccounts.FindAsync(id);

            if (bankAccount == null)
                return NotFound();

            if (bankAccount.HasFunds(withdrawal.Amount.GetValueOrDefault()))
            {
                bankAccount.WithdrawFunds(withdrawal.Amount.GetValueOrDefault());
                await _bankDbContext.SaveChangesAsync(cancellationToken);
                return Ok();
            }

            return BadRequest("Insufficient Funds to make withdrawal.");
        }
    }
}