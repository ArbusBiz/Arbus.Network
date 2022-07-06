# Arbus.Network
[![Build Status](https://dev.azure.com/arbus/Arbus.Network/_apis/build/status/Arbus.Network?branchName=refs%2Fpull%2F1%2Fmerge)](https://dev.azure.com/arbus/Arbus.Network/_build/latest?definitionId=40&branchName=refs%2Fpull%2F1%2Fmerge)

Working with an HttpClient in C# could be quite tricky as for new and as for experienced software developers. Network code usually appeares to be duplicated in multiple projects. Such code is hard to maintain and improve.
At Arbus, we are excited to share the network code we've been working on for more than two years. Our goal was to create a network library that can be used in wide range of solutions. There're two principles of software development we mostly kept in our minds: DRY and Single-reponsibility principiles. We beleave an Open Source community will attract developers like we are. Everyone is welcome to contribute. Let's build a better world together.

## Features
- ApiEndpoints is the perfect place for request/response pair details.
- HttpTimeout handling.
- Built-in JSON support.
- [ProblemDetails](https://tools.ietf.org/html/rfc7807) format support for not success responses.
- Build-in NetworkExceptions.
- Highly customizable for different needs

## How to use
1. Provide your own implementation or use default one for interfaces: INetworkManager, INativeHttpClient, IDefaultHttpClient, IHttpClientContext.
2. Use ApiEndpoint as base class for your API endpoints.
Take a look at samples in the repo's root.

## Downloads

The latest stable release of Arbus.Network is [available on NuGet](https://www.nuget.org/packages/Arbus.Network/).
