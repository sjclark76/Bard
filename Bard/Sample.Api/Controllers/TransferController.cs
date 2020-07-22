using System.Threading;
using System.Threading.Tasks;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Testing.Sample.Api.Controllers
{
    [ApiController]
    [Route("api/transfers")]
    public class TransferController : ControllerBase
    {
        private readonly BankDbContext _dbContext;

        public TransferController(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transfer>> Get(int id)
        {
            var transfer = await _dbContext.Transfers.FindAsync(id);

            return Ok(transfer);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Transfer transfer, CancellationToken cancellationToken = default)
        {
            var fromBankAccount = await _dbContext.BankAccounts.FindAsync(transfer.FromBankAccountId);
            var toBankAccount = await _dbContext.BankAccounts.FindAsync(transfer.ToBankAccountId);

            if (fromBankAccount.HasFunds(transfer.Amount))
            {
                await _dbContext.Transfers.AddAsync(transfer, cancellationToken);
                fromBankAccount.WithdrawFunds(transfer.Amount);
                toBankAccount.DepositFunds(transfer.Amount);
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return BadRequest("Insufficient funds");
            }

            return CreatedAtAction(nameof(Get), new {id = transfer.Id}, transfer);
        }
    }
}