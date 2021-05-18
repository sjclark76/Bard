[![Join the chat at https://gitter.im/Bard-NET/Bard](https://badges.gitter.im/Bard-NET/Bard.svg)](https://gitter.im/Bard-NET/Bard?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)  ![.NET Core](https://github.com/sjclark76/bard/workflows/.NET%20Core/badge.svg?branch=master)    [![NuGet Badge](https://buildstats.info/nuget/Bard)](https://www.nuget.org/packages/Bard/)

# Introduction

Bard is a .NET library for API testing.

## Documentation

Get the full documentation [here](https://bard-1.gitbook.io/bard/)

## Have A Play

[DotNet Fiddle](https://dotnetfiddle.net/O8TE31)

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
### Build Your Own Domain Specific Language
Bard provides you with the building blocks to allow you to build your own Domain Specific Language (DSL) for test arrangement. This allows the developer to build up a library of stories that can be composed via a fluent interface.

The fluent interface guides the developer how to construct the scenario in a logical sequence making it easier for new tests to be written. This arguably involves a little more effort up front but can pay dividends in the long run when working against a complex domain that involves intricate test arrangement.

 Bard uses a functional approach to test arrangement and employs the Collection Pipeline Pattern. 
 
> Collection pipelines are a programming pattern where you organize some computation as a sequence of operations which compose by taking a collection as output of one > operation and feeding it into the next. - Martin Fowler
This means that when there are a number of sequential steps performed during the arrangement of the test data can flow from one step to the next step.
```c#
[Fact]
public void Retrieving_a_bankAccount_by_id_should_return_a_200_ok_response()
{
    Given       
       .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
       .Deposit_has_been_made(100)
       .Withdrawal_has_been_made(50)
       .GetResult(out BankingStoryData bankAccount);
       
    When
       .Get($"api/bankaccounts/{bankAccount.BankAccountId}");
      
    Then.Response
       .ShouldBe
       .Ok<BankAccount>();       
}
```
## First Class Logging
Bard generates exceptional automatically generated log from your tests. This means you can write your tests first and then build your APIs. This gives the developer the opportunity to 'Dog Food' their API whilst writing their tests. No more eyeballing API responses in tools such as Postman.

```javascript
**********************************************************************
*              GIVEN THAT BANK ACCOUNT HAS BEEN CREATED              *
**********************************************************************

REQUEST: POST http://localhost/api/bankaccounts
{
  "id": 0,
  "isActive": false,
  "customerName": "Fred",
  "balance": 0.0
}

RESPONSE: Http Status Code:  Created (201)
Header::Location http://localhost/api/bankaccounts/1
{
  "id": 1,
  "isActive": false,
  "customerName": "Fred",
  "balance": 0.0
}

**********************************************************************
*                        DEPOSIT HAS BEEN MADE                       *
**********************************************************************

REQUEST: POST http://localhost/api/bankaccounts/1/deposits
{
  "id": null,
  "amount": 100.0
}

RESPONSE: Http Status Code:  OK (200)

**********************************************************************
*                      WITHDRAWAL HAS BEEN MADE                      *
**********************************************************************

REQUEST: POST http://localhost/api/bankaccounts/1/withdrawals
{
  "id": null,
  "amount": 50.0
}

RESPONSE: Http Status Code:  OK (200)

**********************************************************************
*                                WHEN                                *
**********************************************************************

REQUEST: GET http://localhost/api/bankaccounts/1

**********************************************************************
*              THEN THE RESPONSE SHOULD BE HTTP 200 OK               *
**********************************************************************

RESPONSE: Http Status Code:  OK (200)
{
  "id": 1,
  "isActive": false,
  "customerName": "Fred",
  "balance": 75.0
}
```

## Fluent Assertions
A fluent API response helper is built directly into Bard. This hides away the boiler plate code needed to work with the HTTP Response and makes the intent of the test easier to comprehend.
```c#
   Then
        .Response
        .ShouldBe
        .BadRequest
        .WithMessage("Insufficient Funds to make withdrawal.");

```



