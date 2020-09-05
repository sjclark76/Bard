# Headers

Bard allows you to assert that the correct headers are present in your API Response.

```csharp
[Fact]
public void Should_included_content_type_header_with_correct_value()
{
   When.Get(URL);

   Then
       .Response
       .Headers
       .ShouldInclude("Content-Type", "application/json; charset=utf-8");
           
}
```

