using LatiumMarketplace.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Hubs
{
    public class Broadcaster : Hub<IBroadcaster>
    {
        public override Task OnConnected()
        {
            // Set connection id for just connected client only
            return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
        }

        // Server side methods called from client, since this is a page by page web application
        // the connection id everytime the page is refreshed hence they will have to subscribe
        // everytime the page is loaded and unsuscribe everytime the page is redirected.
        public Task Subscribe(string chatroom)
        {
            return Groups.Add(Context.ConnectionId, chatroom.ToString());
        }

        public Task Unsubscribe(string chatroom)
        {
            return Groups.Remove(Context.ConnectionId, chatroom.ToString());
        }
    }

    // Client side methods to be invoked by Broadcaster Hub
    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId); // Don't worry about this method, as can be seen above this is called automatically everytime a connection is made
        // Define all client side functions
        Task PresentNotification(Notification notification);
    }
}
