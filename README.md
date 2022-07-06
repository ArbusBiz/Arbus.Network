# Arbus.Network 
[![Build Status](https://dev.azure.com/arbus/Arbus.Network/_apis/build/status/Arbus.Network?branchName=refs%2Fpull%2F1%2Fmerge)](https://dev.azure.com/arbus/Arbus.Network/_build/latest?definitionId=40&branchName=refs%2Fpull%2F1%2Fmerge)

The library helps to start sending and receiving http requests via HttpClient just in a couple of steps. 

## Features ##
- ApiEndpoints is the perfect place to the request/response details.
- HttpTimeout timeout handling.
- Built-in JSON support.
- [ProblemDetails](https://tools.ietf.org/html/rfc7807) format support for not success responses.
- Build-in NetworkExceptions.

## How to use ##
1. Provide your own implementation or use default one for interfaces: INetworkManager, INativeHttpClient, IDefaultHttpClient, IHttpClientContext.
2. Use ApiEndpoint as base class for your API endpoints.
Take a look at samples in the repo's root.

## Downloads ##

The latest stable release of Arbus.Network is [available on NuGet](https://www.nuget.org/packages/Arbus.Network/).
