# HTTP Response

## HTTP Response Assertion

Bard natively supports out of the box the following HTTP responses

* **HTTP 200 OK**
* **HTTP 201 Created**
* **HTTP 204 No Content**
* **HTTP 403 Forbidden**
* **HTTP 404 Not Found**

```csharp
 Then
     .Response
     .ShouldBe
     .Ok();
 
 // OR
 
 Then
     .Response
     .ShouldBe
     .Forbidden();
```

If you need to Assert another HTTP code was returned then you can specify the HTTP code as well.

```csharp
 Then
     .Response
     .ShouldBe
     .StatusCodeShouldBe(HttpStatusCode.Ambiguous);
```

### 

