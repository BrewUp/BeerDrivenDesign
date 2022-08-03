namespace BeerDrivenDesign.Api.Transport.RabbitMq.Settings;

public class RabbitMqSettings
{
    public string ExchangeName { get; set; } = string.Empty;
    public string BrokerUrl { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
}