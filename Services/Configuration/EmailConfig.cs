using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Configuration
{
    public class EmailConfig
    {
        public string From { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
