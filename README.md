# Arbus.Network
[![Build Status](https://dev.azure.com/arbus/Arbus.Network/_apis/build/status/Arbus.Network?branchName=master)](https://dev.azure.com/arbus/Arbus.Network/_build/latest?definitionId=40&branchName=master)

Working with an HttpClient in C# could be quite tricky as for new and as for experienced software developers. Network code usually appears to be duplicated in multiple projects. Such code is hard to maintain and improve.  
At Arbus, we are excited to share the network code we've been working on for more than two years. We wanted to write network code that'll be possible to reuse in wide range of solutions. There're two principles of software development we mostly kept in our minds: DRY and Single-responsibility principles. We believe an Open Source community will attract passionate developers like us. Everyone is welcome to contribute. 

Let's build a better world together!

## Features
- ApiEndpoints is the perfect place for request/response pair details.
- HttpTimeout handling.
- Built-in JSON support.
- [ProblemDetails](https://tools.ietf.org/html/rfc7807) format support for non-success responses.
- Built-in NetworkExceptions.
- Highly customizable for different needs

## How to use
1. Provide your own implementation or use default one for interfaces: INetworkManager, INativeHttpClient, IHttpClientContext:
   ```c#
   _networkManager = new WindowsNetworkManager();
   _nativeHttpClient = new WindowsHttpClient();
   _httpClientContext = new HttpClientContext(_nativeHttpClient);

2. Use ApiEndpoint as base class for your API endpoints:
   ```c#
   public class GetAllOrdersApiEndpoint : ApiEndpoint<OrdersResponseDto>
   {
       public GetAllOrdersApiEndpoint()
           : base(HttpMethod.Get, "https://example.com/api/v1/orders")
       {
       }
   }
   ```

3. Run your endpoint:
   ```c#
   GetAllOrdersApiEndpoint endpoint = new();
   var orders = await _httpClientContext.RunEndpoint(endpoint).ConfigureAwait(false);
   ```

  Take a look at samples in the repo's root.

## Downloads

The latest stable release of Arbus.Network is [available on NuGet](https://www.nuget.org/packages/Arbus.Network/).
