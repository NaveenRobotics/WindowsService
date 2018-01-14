using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWindowsService
{
    
    class Message
    {
        public string PC_Name { get; set; }
        public string Time { get; set; }
        public string Log_Message { get; set; }

        public Message(string Log_Message)
        {
            this.PC_Name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.Time = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");
            this.Log_Message = Log_Message;
        }
    }
}
