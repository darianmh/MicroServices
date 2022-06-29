using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public static class RabbitConfiguration
    {
        public static string Uri = "172.16.20.14";
        public static string Username = "admin";
        public static string Password = "12345678";
        public static string QueueName = "DarianTestQ";
    }
}
