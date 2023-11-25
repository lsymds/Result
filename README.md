# Result

A Result monad implementation in .NET. Build more resilient applications by being explicit with what errors can be
returned from your methods.

# Getting Started

## Installing

Install the NuGet package:

```bash
dotnet add package LSymds.Result
```

## Creating results

You create results by calling `Success` or `Erroneous` on the `Result<TSuccess, TError>` type which builds a result
that is in a successful or erroneous state respectively.

For example, to create a successful `Result<string, MyErrorCode>`:

```csharp
return Result<string, MyErrorCode>.Successful("Success!");
```

and to create an erroneous `Result<string, MyErrorCode>`:

```csharp
return Result<string, MyErrorCode>.Erroneous(MyErrorCode.Broken);
```

### Creating results without a successful return type

To keep this library and any implementing code simple and easy to understand, I made the decision to implement one
consistent interface (`Result<TSuccess, TError>`) regardless of whether `TSuccess` was a value of any substance or not.

As C# does not contain a `Nothing` type, this library provides one.

If you wish to return a result with a 'void' success type, you can do the following:

```csharp
return Result<Nothing, MyErrorCode>.Successful(Nothing.Instance);
```

## Utilities

Numerous utility methods hang off of the `Result<TSuccess, TError>` type.

- `Map` - Returns a new result with the success type as the provided generic type, mapping the existing data value using the provided function and leaving the error value (if present) unchanged.
- `MapError` - Performs similarly to Map, except it maps the error type and error value, leaving the data value (if present) unchanged.
- `Unwrap` - **UNSAFELY** retrieves the value of `Data`, throwing an `InvalidOperationException` if the result is erroneous.
- `UnwrapOrElse` - Retrieves the value of `Data` if the result is successful or the provided value if it isn't.
