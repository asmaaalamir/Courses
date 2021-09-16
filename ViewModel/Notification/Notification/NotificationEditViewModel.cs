
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModel
{
    public class NotificationEditViewModel
    {
        public string ID { get; set; }

        public string UserID { get; set; }
        public bool IsRead { get; set; } = false;
        public string Message { get; set; }
        public string Data { get; set; }


    }
}
