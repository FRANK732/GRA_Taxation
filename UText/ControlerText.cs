using GRA_Taxation.Controllers;
using GRA_Taxation.Infrastructure;
using GRA_Taxation.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;

namespace GRA_Taxation.UText;

public class ControllerTest
{
    private readonly Mock<Taxation> _taxationMock;
    private readonly TaxController _controller;

    public ControllerTest()
    {
        _taxationMock = new Mock<Taxation>();
        _controller = new TaxController(_taxationMock.Object);
    }

    [Fact]
    public void Calculation_ReturnsOkResult_WithValidInput()
    {
        // Arrange
        var input = new TaxInput
        {
            DesiredNet = 200,
            Allowances = 50
        };

        var mockResponse = new Response
        {
            GrossSalary = 120,
            BasicSalary = 0,
            TotalPayeTax = 1000,
            EmployeePensionContribution = 500,
            EmployerPensionContribution = 600,
            NETSalary = 10000
        };

        _taxationMock.Setup(t => t.Calculate(input)).Returns(mockResponse);

        // Act
        var result = _controller.Calculation(input);

        // Assert
        var okResult = Assert.IsType<Ok<Response>>(result);
        var response = Assert.IsType<Response>(okResult.Value);
        Assert.Equal(mockResponse,response);
        Assert.NotNull(response);
    }
}