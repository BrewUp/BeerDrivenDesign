namespace BeerDrivenDesign.Api.Shared.Configuration;

public class AppSettings
{
    public EventStoreSettings EventStoreSettings { get; set; } = new();
}

public class EventStoreSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}