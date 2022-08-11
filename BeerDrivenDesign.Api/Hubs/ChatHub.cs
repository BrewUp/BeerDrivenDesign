using BeerDrivenDesign.Api;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHubContext<ChatHub> _context;
        public ChatHub(IHubContext<ChatHub> context)
        {
            _context = context;
        }

        //private int count;
        //private bool run = true;
        //public override async Task OnConnectedAsync()
        //{
        //    if (count == 0)
        //    {
        //        SendToGui();
        //    }


        //    await base.OnConnectedAsync();
        //}


        //public async Task SendToGui()
        //{
        //    while (run)
        //    {
        //        count++;
        //        await Clients.All.SendAsync("SetCounter", count);
        //        Thread.Sleep(1000);
        //    }

        //}
        //public override async Task OnDisconnectedAsync(Exception? exception)
        //{
        //    run = false;
        //}

        public async Task AddMessageToChat(int count)
        {
            //await Clients.All.SendAsync("GetThatMessageDude", user, message);
            await _context.Clients.All.SendAsync("SetCounter", count);
        }

        internal async Task AddMessageToChat(CounterService.Count count)
        {
            await _context.Clients.All.SendAsync("SetCounter", count);
        }
    }
}
