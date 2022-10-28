using BeerDrivenDesign.Modules.Purchases.CustomTypes;

namespace BeerDrivenDesign.Modules.Purchases.Tests;

public class CustomTypesTests
{
    [Fact]
    public void Verify_ValueObjects_Equality()
    {
        //var order1 = new PurchaseOrderEntity(new OrderId("123"), new OrderNumber("456"));
        //var order2 = new PurchaseOrderEntity(new OrderId("123"), new OrderNumber("456"));

        //Assert.True(order1.Equals(order2));
    }

    [Fact]
    public void Test_Superfluo()
    {
        //var order1 = new PurchaseOrderEntity(new OrderId("123"), new OrderNumber("456"));

        //Assert.True(order1.OrderId.Value.Equals("123"));
        //Assert.True(order1.OrderNumber.Value.Equals("456"));
    }
}