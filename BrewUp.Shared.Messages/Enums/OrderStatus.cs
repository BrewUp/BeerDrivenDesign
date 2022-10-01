namespace BrewUp.Shared.Messages.Enums;

public class OrderStatus : Enumeration
{
    public static OrderStatus Open = new(0, "O", "Open");
    public static OrderStatus Completed = new(1, "C", "Completed");

    public static IEnumerable<OrderStatus> List() => new[] { Open, Completed };

    public OrderStatus(int id, string code, string name) : base(id, code, name)
    {
    }

    public static OrderStatus FromName(string name)
    {
        var statusCode = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (statusCode is null)
            throw new Exception($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

        return statusCode;
    }
    public static OrderStatus FromCode(string code)
    {
        var statusCode = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

        if (statusCode is null)
            throw new Exception($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Code))}");

        return statusCode;
    }

    public static OrderStatus From(int id)
    {
        var statusCode = List().SingleOrDefault(s => s.Id == id);

        if (statusCode is null)
            throw new Exception($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

        return statusCode;
    }
}
