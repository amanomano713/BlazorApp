using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string IdPuja)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message , IdPuja);
        }
    }
}
