# Configuration



## Configure The Scenario

So before we can start we need to configure our Scenario. To do this we are going to need provide a number of things.

```csharp
 var scenario = GrpcScenarioConfiguration
                .UseGrpc<MyGrpcClient>()
                .WithStoryBook<MyStoryBook, MyStoryData>()
                .Configure(options =>
                {
                    options.Services = _host.Services;
                    options.LogMessage = s => _output.WriteLine(s);
                    options.GrpcClient = c => new MyGrpcClient(c);
                    options.Client = _httpClient;
                });
```

1. On line 2 we specify our grpc generated client.
2. Line 3 we specify our StoryBook & Story Data
3. Line 6 we provide an instance of our Service Provider so we can use dependency injection within our Stories \(Optional\)
4. Line 7 we specify how to log our output \(Recommended\)
5. Line 8 provides a delegate function to instantiate an instance of our gRPC client \(Required\)
6. Line 9 provides an instance of our HTTP Client.

