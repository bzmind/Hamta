using System.Collections.Generic;
using Domain.Order_Aggregate;
using Domain.Shared.BaseClasses;
using Xunit;

namespace Domain.Tests.OrderTests;

public class OrderTests
{
    [Fact]
    public void dd()
    {
        var order = new Order(1, new List<OrderItem>());
    }
}