namespace Fluent.Testing.Sample.Api.Model
{
    public class Transfer
    {
        public int? FromBankAccountId { get; set; }
        public int? ToBankAccountId { get; set; }
        public decimal Amount { get; set; }
        public int Id { get; set; }
    }
}