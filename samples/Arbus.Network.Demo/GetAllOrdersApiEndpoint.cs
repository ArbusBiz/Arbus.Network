namespace Arbus.Network.Demo;

public class GetAllOrdersApiEndpoint : ApiEndpoint<OrdersResponseDto>
{
    public GetAllOrdersApiEndpoint() : base(HttpMethod.Get, "https://example.com/api/v1/orders")
    {
    }
}