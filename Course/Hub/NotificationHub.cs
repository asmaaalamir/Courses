
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using Service;
using ViewModel;

namespace Course
{
    public  class NotificationHub
    {
        private readonly IHubContext<CourseHub> _hubContext;
        private readonly IHubClientService _hubClientService;
        private readonly INotificationService _NotificationService;
        private readonly IStudentService _StudentService;
     
        public NotificationHub(IHubContext<CourseHub> hubContext, 
            IHubClientService hubClientService,
           INotificationService NotificationService, IStudentService  StudentService)
        {

            _hubContext = hubContext;
            _hubClientService = hubClientService;
            _NotificationService = NotificationService;
            _StudentService = StudentService;
           
        }
        


        public async void UpdateCourse(CourseEditViewModel model)
        {
           
            var StudentIDs = _StudentService.GetStudents(model.ID);
            foreach (var ID in StudentIDs)
            {
                _NotificationService.Add(new NotificationEditViewModel() { ID = null, UserID=ID,IsRead=false,Message="update course ${model.Name}"});
                var hubClients = _hubClientService.Get(ID);
                if (hubClients != null)
                    foreach (var item in hubClients)
                    {
                        await _hubContext.Clients.Client(item.ConnectionId).SendAsync("UpdateCourse", model);
                    }
            }
        }

    }
}
