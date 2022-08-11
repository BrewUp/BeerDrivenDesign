using BlazorChatSignalR.Server.Hubs;

namespace BeerDrivenDesign.Api;

public class CounterService
{
    private readonly ChatHub _hub;
    private int count;

    public CounterService(ChatHub hub)
    {
        _hub = hub;
    }
    public void StartCount()
    {
        Console.WriteLine("Entro nel cliclo");
        while (true)
        {
            count++;
            _hub.AddMessageToChat(new Count(count, DateTime.UtcNow, "Pippo"));
            Console.WriteLine(count);
            Thread.Sleep(1000);

        }

    }

    public record Count(int Value, DateTime Data, string Nome);
}
