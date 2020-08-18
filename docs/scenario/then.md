# Then

This is the test assertion part of your test. 

Bard provides a fluent API for working with your API responses. You can Assert that the response is as expected.

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

### Bad Requests

The response can be checked to ensure the correct error message or error code is returned by the API.

When your API returns an HTTP 400 Bard give you the ability to interrogate the API response in more detail. 

