# Performance Testing

Bard provides the ability to assert that the API responses are returned within a specified timeframe. This can be asserted at a test level or globally for an entire Scenario.

## Individual API Call Assertion 

The test below show an individual assertion for a specific end point.

```csharp
[Fact]
public void The_slow_running_endpoint_should_return_within_two_seconds()
{
    When
        .Get("api/slow-running-endpoint");
   
    Then
        .Response
        .Time
        .LessThan(2000); // Time in milliseconds.
    
}
```

## Global Configuration

The scenario configuration below demonstrates how to set a global benchmark for all API calls so that they do not exceed 2000 milliseconds.

```csharp
var scenario = ScenarioConfiguration
    .Configure(options =>
    {
        options.MaxApiResponseTime = 2000;
        // Other configuration goes here..
    });
```

