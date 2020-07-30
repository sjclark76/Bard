# StoryBook

As described in the introduction a `StoryBook` is the starting `Chapter` of our `Scenario`.

Lets illustrate what that really means, with an example. I will use my fictitious Banking API to demonstrate.

We want to test the scenario in our Banking API that the balance of our account is correct after we make a cash deposit. 

Before we can do that there are a couple of steps we need to do to test that scenario.

1. Create the bank account
2. Deposit the money into the correct account.

In Bard after we have created the bank account we can pass the results from the '_Create Bank Account_' story into the '_Deposit Money_' story this is useful because we probably need to know the Id of the newly created bank account.

Lets demonstrate what that would look like with a code example.

```csharp
public BankAccountHasBeenCreated BankAccount_has_been_created()
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

Lets talk about what's going on in that example. The first thing to note is we are calling the base method `When` and this is where we do the 'work' of the story is done by calling our create bank account endpoint. Notice that we are making our API call using our `TestContext` on line 10.

Now on line 12  we are returning we are returning the `BankAccount` response from the API call.

```csharp
return response.Content<BankAccount>();
```

We could return any object we want here but convenience we are simply returning the response from the API. The `BankAccount` object we return here will be the input to the next chapter.

Finally on line 14 we call the generic`Then` base method. This instructs our fluent API what the next `Chapter` is that will help us build out our Fluent Test interface.

```csharp
.Then<BankAccountHasBeenCreated>();
```





