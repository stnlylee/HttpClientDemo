# Implementation Notes

This demo has following features:

- How to run a .NET Core console app with dependency injection
- How to use HttpClient to call 3rd party API

Demo 3rd party API is returning this:

```json
[{ "id": 53, "first": "Bill", "last": "Bryson", "age":23, "gender":"M" },
{ "id": 62, "first": "John", "last": "Travolta", "age":54, "gender":"M" },
{ "id": 41, "first": "Frank", "last": "Zappa", "age":23, gender:"T" },
{ "id": 31, "first": "Jill", "last": "Scott", "age":66, gender:"Y" },
{ "id": 31, "first": "Anna", "last": "Meredith", "age":66, "gender":"Y" },
{ "id": 31, "first": "Janet", "last": "Jackson", "age":66, "gender":"F" }]
```

In `UserApiDatasource.cs`:

- It will distinct the id field then validate gender field
- It will inject `IHttpClientFactory` as default implementation and create `HttpClient` using it (`IHttpClientFactory` will reuse instance with same name)
- Convert data to `UserDto` object using `JsonConvert`
- Use level 1 cache here

In `AppConfiguration.cs`:

- Use `Polly` policies for `Timeout`, `WaitAndRetry` and `CircuitBreaker`
- Register a `HttpClient` called "UserClient" and reading config for base url

Useful links

- DI in console app - https://keestalkstech.com/2018/04/dependency-injection-with-ioptions-in-console-apps-in-net-core-2/ and https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
- Everything for HttpClient - https://code-maze.com/httpclient-with-asp-net-core-tutorial/
- Inject instance for multiple implementation of same interface - https://devkimchi.com/2020/07/01/5-ways-injecting-multiple-instances-of-same-interface-on-aspnet-core/
