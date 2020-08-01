
# Introduction

Bard is a .NET library for API testing.

gRPC Support coming soon!!

## Documentation

Get the full documentation [here](https://bard-1.gitbook.io/bard/)

## Installation

```text
Install-Package Bard
```

Or using the .net core CLI from a terminal window:

```text
dotnet add package Bard
```

## Why Bard?

Bard is a test library written by Developers for Developers. It is as much a development tool as it is a test library. Although Bard is a .NET library it can be used to test any API if you want.

### First Class Test Logging

Bard generates exceptional test logging. This means you can write your tests first and then build your APIs. This gives the developer the opportunity to 'Dog Food' their API whilst writing their tests. No more using tools such as Postman to eyeball the API responses.

```javascript
*******************************************
* GIVEN THAT BankAccount_has_been_created *
*******************************************

REQUEST: POST api/bankaccounts
{  
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
   "amount": 100.0
}

RESPONSE: Http Status Code:  OK (200)
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 100.0
}
****************************************
*             WHEN                     *
****************************************

GET api/bankaccounts/1
RESPONSE: Http Status Code:  OK (200)
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 75.0
}

```

### Fluent Scenarios

Bard has a fluent interface builder that allows the developer to build up a library of stories that can be composed via a fluent interface.

The fluent interface guides the developer how to build the scenario making it easier for new tests to be written. Arguably this involves more effort up front but this eventually pays dividends when working against a complex domain that involves intricate test arrangement.

Because Bard uses a functional approach to test arrangement and employs the [Collection Pipeline Pattern](https://martinfowler.com/articles/collection-pipeline/) 

> Collection pipelines are a programming pattern where you organize some computation as a sequence of operations which compose by taking a collection as output of one operation and feeding it into the next. - Martin Fowler

This means that when employing a number of steps to set up a test the output of the first test step is the input to the next. This makes your tests easier to write and understand.

```csharp
  Given
       .That
       .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
       .Deposit_has_been_made(100)
       .And()
       .Withdrawal_has_been_made(50)
       .Deposit_has_been_made(25);
```

### Smart HTTP Response Assertions

A fluent API response helper is built directly into Bard. This hides away the boiler plate code when dealing with an HTTP Response, making the tests easier to write and easier to understand.

```csharp
    Then
        .Response
        .ShouldBe
        .BadRequest
        .WithMessage("Insufficient Funds to make withdrawal.");
```






![.NET Core](https://github.com/sjclark76/bard/workflows/.NET%20Core/badge.svg?branch=master)
