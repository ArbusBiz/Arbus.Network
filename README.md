# Arbus.Network 
[![Build Status](https://dev.azure.com/arbus/Arbus.Network/_apis/build/status/Arbus.Network?branchName=refs%2Fpull%2F1%2Fmerge)](https://dev.azure.com/arbus/Arbus.Network/_build/latest?definitionId=40&branchName=refs%2Fpull%2F1%2Fmerge)

The library helps to start sending and receiving http requests via HttpClient just in a couple of steps. 
To begin:
1. Provide your own implementation or use default one for interfaces: INetworkManager, IDefaultHttpClient, INativeHttpClient.
2. Use ApiEndpoint as base class for your API endpoints. The main purpose of ApiEndpoint class to provide a great way of separation of concerns.
