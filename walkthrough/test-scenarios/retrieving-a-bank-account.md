# Retrieving a bank account

Lets move onto testing our [GET Bank Account](../../sample-banking-api/retrieving-a-bank-account.md) API endpoint. This is a little bit more complicated as to test the endpoint we need have a bank account in the system to retrieve. So lets add a [Chapter ](../../scenario/given/chapter.md)to our `StoryBook`Class that we created in the previous step and add in the following code.

```csharp
public class BankingStory : StoryBook
{
    public BankAccountHasBeenCreated BankAccount_has_been_created(Action<BankAccount>? configureBankAccount = null)
    {
        return When(context =>
        {
            var bankAccount = new BankAccount
            {
                CustomerName = "Ranulph Fiennes"
            };
            var response = context.Api.Post("api/bankaccounts", bankAccount);

            return response.Content<BankAccount>();
         })
        .Then<BankAccountHasBeenCreated>();
        }
```



