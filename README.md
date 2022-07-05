# Arbus.Network

The library helps to start sending and receiving http requests via HttpClient just in a couple of steps. To begin:

Provide your own implementation or use default one for interfaces: INetworkManager, IDefaultHttpClient, INativeHttpClient.
Use ApiEndpoint as base class for your API endpoints. The main purpose of ApiEndpoint class to provide a great way of separation of concerns.
