# Change Log

## 2.0.0 - 1-08-2020

### Changed

* Changed method signatures on Chapters to make them simpler. The Funcs used to require the Chapter Input in the method signature. Now the Chapter input is accessible on the ScenarioContext
* Changed UseResult method to access tests output. Used to be called UseResult but is now called GetResult. 

```csharp
.UseResult(account => bankAccount = account);
```

Now uses:

```csharp
.GetResult(out BankAccount? bankAccount);
```

**Note:** UseResult used to terminate the test arrangement. Now GetResult allows you to continue with the test arrangement.

* Removed CommonExtensions so now cannot use .And .A etc between test arrangements.
* 
## 1.2.0 - 28-07-2020

### Added

* Added better logging when building a chapter and not calling the Context's API. Previously nothing was logged to the console. Now Bard tracks if the API has been called or not and if it hasn't it defaults to outputing the response from the story.

## 1.1.1 - 26-07-2020

### Fixed

* Bug when asserting HTTP code returned from API due to Shouldly not working in Release build.

### Changed

* Removed dependency on [Shouldly ](https://shouldly.readthedocs.io/en/latest/)library.

## 1.1.1 - 26-07-2020

### Fixed

* Bug when asserting HTTP code returned from API due to Shouldly not working in Release build.

### Changed

* Removed dependency on [Shouldly ](https://shouldly.readthedocs.io/en/latest/)library.

## 1.0.0 - 25-07-2020

Initial Release



