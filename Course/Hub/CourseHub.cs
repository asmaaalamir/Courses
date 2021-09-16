

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Service;
using ViewModel;

namespace Course
{
    public class CourseHub : Hub
    {


        private readonly IHubClientService _hubClientService;
        public CourseHub(IHubClientService hubClientService)
        {
            _hubClientService = hubClientService;

        }
      



        public override Task OnConnectedAsync()
        {

            string token = Context.GetHttpContext().Request.Query["access_token"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var asd = tokenS.Claims.ToList();
            var userID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
            var role = tokenS.Claims.First(claim => claim.Type == "role").Value;
            if (!string.IsNullOrEmpty(token) && role== "Student")
            {
                _hubClientService.Add(new HubClientEditViewModel()
                {
                    UserID = userID,
                    ConnectionId = Context.ConnectionId,
                });
            } 
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception stopCalled)
        {
            
            _hubClientService.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(stopCalled);
        }

    

    }
}
