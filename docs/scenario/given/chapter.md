# Chapter

A `Chapter` is the decision point in our `Scenario`. It contains Stories. It allows us to guide the test author what they can do next.

The important thing to remember is that the `Chapter` we are going to must take the output of our previous `Story` as an input. We do that by inheriting from the base Class `Chapter` and use the Bank Account as the Generic Type. Our class should look like this.

```csharp
public class BankAccountHasBeenCreated : Chapter<BankAccount>
{
}
```

Now we have our Chapter we can add our Stories. 

