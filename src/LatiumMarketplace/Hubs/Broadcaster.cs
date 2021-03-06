﻿using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.MessageViewModels;
using Microsoft.EntityFrameworkCore;

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
        public Task Subscribe(string email)
        {
            return Groups.Add(Context.ConnectionId, email.ToString());
        }

        public Task Unsubscribe(string email)
        {
            return Groups.Remove(Context.ConnectionId, email.ToString());
        }
    }

    // Client side methods to be invoked by Broadcaster Hub
    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId); // Don't worry about this method, as can be seen above this is called automatically everytime a connection is made
        // Define all client side functions
        Task AddNotificationToQueue(Notification notification);
        Task UpdateOverallNotificationCount();
        Task CheckBan();

    }
}
