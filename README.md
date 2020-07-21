# Bard

Bard is an opinionated .NET library for writing API tests in a fluent and human readable manner.

The philosophy behind Bard is it's a tool for developers and the emphasis has been on making a library that is a development tool as much as a test tool. 

Typically a test would look something like this.

```C#
   Given
        .That    
	.BankAccount_has_been_created(account => account.CustomerName = "Dougal")
	.Deposit_has_been_made(100)
	.UseResult(account => customerId = account.Id.GetValueOrDefault());

    When
	.Get($"{ApiBankaccounts}/{customerId}");
        
    Then
	.Response
        .ShouldBe
        .Ok<BankAccount>()
        .Balance
        .ShouldBe(100);
```
A lot of effort has been put into providing really good logging which allows the developer to 'dog food' their API whilst they're building and testing it.

And the log output would looks something like this.

```
*********************************************
* Given That A BankAccount_has_been_created *
*********************************************

REQUEST: POST api/bankaccounts
{
  "id": null,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 0.0
}
RESPONSE: Http Status Code:  Created (201)
Header::Location http://localhost/api/bankaccounts/1
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 0.0
}
******************************
*  And Deposit_has_been_made *
******************************

REQUEST: POST api/bankaccounts/1/deposits
{
  "id": null,
  "amount": 100.0
}
RESPONSE: Http Status Code:  OK (200)
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 100.0
}
GET api/bankaccounts/1
RESPONSE: Http Status Code:  OK (200)
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 75.0
}
```

testing testing


![.NET Core](https://github.com/sjclark76/bard/workflows/.NET%20Core/badge.svg?branch=master)
