using Castle.Core.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace giveBloodNewApp.Services
{
    public class MyConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Address { get; set; }
        public string fromPassword { get; set; }
    }

}

