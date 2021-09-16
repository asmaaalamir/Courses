
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("HubClient", Schema = "Notification")]
    public class HubClient : BaseModel
    {
       
        public string UserID { get; set; }
        public string ConnectionId { get; set; }
       

    }
}
