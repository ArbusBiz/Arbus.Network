using Moq;

namespace Arbus.Network.UnitTests;

public abstract class TestFixture
{
    private readonly MockRepository _mockRepository = new(default);

    public Mock<T> CreateMock<T>(MockBehavior mockBehavior = MockBehavior.Strict) where T : class
        => _mockRepository.Create<T>(mockBehavior);
}
