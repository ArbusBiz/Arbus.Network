namespace Arbus.Network.UnitTests;

[TestFixture]
public abstract class TestFixture
{
    private readonly MockRepository _mockRepository = new(default);

    public Mock<T> CreateMock<T>(MockBehavior mockBehavior = MockBehavior.Strict, params object[] args) where T : class
        => _mockRepository.Create<T>(mockBehavior, args);

    [TearDown]
    public void AfterEachTest()
    {
        _mockRepository.VerifyAll();
    }
}