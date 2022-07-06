using Arbus.Network.Abstractions;

namespace Arbus.Network.Demo;

public class GetAllOrdersApiEndpoint : ApiEndpoint<OrdersResponseDto>
{
    public override string Path => "https://example.com/api/v1/orders";

    public override HttpMethod Method => HttpMethod.Get;
}