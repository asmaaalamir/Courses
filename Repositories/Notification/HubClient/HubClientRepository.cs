
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class HubClientRepository : Repository<HubClient>, IHubClientRepository
    {
        public HubClientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}