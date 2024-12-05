using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Orders.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
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
        // Arrange
        var request = CreateOrderHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(null));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(UserErrors.UserNotFound(request.UserId), result.Errors[0]);
    }

    [Fact(DisplayName = "Given product not found When creating order Then returns error response")]
    public async Task Handle_ProductNotFound_ReturnsErrorResponse()
    {
        // Arrange
        var request = CreateOrderHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items[0].ProductId, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Product?>(null));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.ProductNotFound(request.Items[0].ProductId), result.Errors[0]);
    }

    [Fact(DisplayName = "Given add item failure When creating order Then returns error response")]
    public async Task Handle_AddItemFailure_ReturnsErrorResponse()
    {
        // Arrange
        var request = CreateOrderHandlerTestData.GenerateInvalidCommand();
        var user = UserTestData.GenerateValidUser();
        var product = new Product("Test Product", 10.0m, request.Items[0].ProductId);

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items[0].ProductId, Arg.Any<CancellationToken>()).Returns(product);
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "Given valid order data When creating order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var request = CreateOrderHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();
        var product = new Product("Test Product", 10.0m, request.Items[0].ProductId);

        _userRepository.GetByIdAsync(request.UserId, Arg.Any<CancellationToken>()).Returns(user);
        _productRepository.GetByIdAsync(request.Items[0].ProductId, Arg.Any<CancellationToken>()).Returns(product);
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
