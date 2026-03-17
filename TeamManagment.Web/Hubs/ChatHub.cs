using Microsoft.AspNetCore.SignalR;

namespace TeamManagment.Web.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    // this method ReceiveMessage will define in js code 
        //    // and take input username to sender and message
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        public async Task SendMessage(string user, string message, string reciverId)
        {
            // this method ReceiveMessage will define in js code 
            // and take input username to sender and message
            //await Clients.All.SendAsync("ReceiveFromUser", user, message,reciverId);
            var conId = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveFromUser" , user,message,reciverId);

        }

    }
}
