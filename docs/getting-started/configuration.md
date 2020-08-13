# Configuration

## Basic Configuration

Basic configuration of Bard means the Arrangement part of the test cannot be utilized.

To configure our Scenario a few thing need to be set.

* **Client**: our HTTP Client to call our API with \(**Required\)**
* **LogMessage**: An action to instruct Bard where to log test output to. This is optional but important because Bard provides detailed logs to the test output windows to help with writing and debugging test failures. If not supplied Bard will output all log messages to the Console window which may not appear in your test output. **\(Optional\)**
* **Services**: An instance of a .NET service provider to allow Bard access to use Dependency Injection. **\(Optional\)**
* **BadRequestProvider:** Override the default bad request provider with your own implementation. **\(Optional\)**

```csharp
var scenario = ScenarioConfiguration
                .Configure(options =>
                {
                     options.Client = httpClient;
                     options.LogMessage = output.WriteLine;
                     options.Services = host.Services;
                     options.BadRequestProvider = new MyBadRequestProvider();
                });
```

## Advanced Configuration

{% hint style="info" %}

{% endhint %}



