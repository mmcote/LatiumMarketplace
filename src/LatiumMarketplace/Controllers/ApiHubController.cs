using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// ApiHubController allows other controllers to inherit from it to be able to send use
    /// notifications. Specifically the functions in the hub function
    /// </summary>
    // https://raw.githubusercontent.com/matthamil/Chatazon/master/Controllers/ApiHubController.cs
    public abstract class ApiHubController<T> : Controller
        where T : Hub
    {
        private readonly IHubContext _hub;
        public IHubConnectionContext<dynamic> Clients { get; private set; }
        public IGroupManager Groups { get; private set; }
        protected ApiHubController(IConnectionManager signalRConnectionManager)
        {
            var _hub = signalRConnectionManager.GetHubContext<T>();
            try
            {
                Clients = _hub.Clients;
                Groups = _hub.Groups;
            }
            catch { }
        }
    }
}
