
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Notification", Schema = "Notification")]
    public class Notification : BaseModel
    {
       
        public string UserID { get; set; }     
        public bool IsRead { get; set; } = false;
        public string Message { get; set; }
        public string Data { get; set; }


    }
  
}