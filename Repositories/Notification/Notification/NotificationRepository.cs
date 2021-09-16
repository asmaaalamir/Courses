
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}