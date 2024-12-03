using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Orders.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Orders;

public class CreateOrderHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _userRepository = Substitute.For<IUserRepository>();
        _orderRepository = Substitute.For<IOrderRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new CreateOrderCommandHandler(
            _unitOfWork,
            _userRepository,
            _orderRepository,
            _productRepository);
    }

    [Fact(DisplayName = "Given user not found When creating order Then returns error response")]
    public async Task Handle_UserNotFound_ReturnsErrorResponse()
    {
        // Given
        var request = CreateOrderHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns((User)null);

        // When
        var result = await _handler.Handle(request, CancellationToken.None);

        // Then
        Assert.False(result.IsSuccess);
        Assert.Equal(UserErrors.UserNotFound(request.UserId), result.Errors.First());
    }

    /// <summary>
    /// Tests that a product not found scenario returns an error response.
    /// </summary>
    [Fact(DisplayName = "Given product not found When creating order Then returns error response")]
    public async Task Handle_ProductNotFound_ReturnsErrorResponse()
    {
        // Given
        var request = CreateOrderHandlerTestData.GenerateValidCommand();
        var user = new User(request.UserId, "Test User");

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items.First().ProductId, Arg.Any<CancellationToken>()).Returns((Product)null);

        // When
        var result = await _handler.Handle(request, CancellationToken.None);

        // Then
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.ProductNotFound(request.Items.First().ProductId), result.Errors.First());
    }

    /// <summary>
    /// Tests that an add item failure scenario returns an error response.
    /// </summary>
    [Fact(DisplayName = "Given add item failure When creating order Then returns error response")]
    public async Task Handle_AddItemFailure_ReturnsErrorResponse()
    {
        // Given
        var request = CreateOrderHandlerTestData.GenerateValidCommand();
        var user = new User(request.UserId, "Test User");
        var product = new Product(request.Items.First().ProductId, "Test Product", 10.0m);

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items.First().ProductId, Arg.Any<CancellationToken>()).Returns(product);
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var order = Order.Create(user, request.Branch);
        order.AddItem(product, 1); // Simulate failure

        // When
        var result = await _handler.Handle(request, CancellationToken.None);

        // Then
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that a valid order creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When creating order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var request = CreateOrderHandlerTestData.GenerateValidCommand();
        var user = new User(request.UserId, "Test User");
        var product = new Product(request.Items.First().ProductId, "Test Product", 10.0m);

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items.First().ProductId, Arg.Any<CancellationToken>()).Returns(product);
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // When
        var result = await _handler.Handle(request, CancellationToken.None);

        // Then
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
