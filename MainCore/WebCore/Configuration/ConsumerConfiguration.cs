using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Infrastructure.Services.Consumer;
using Microsoft.AspNetCore.Builder;

namespace WebCore.Configuration
{
    public static class ConsumerConfiguration
    {
        public static void UseConsumer(this WebApplication webApplication)
        {
            if (ConsumerHelper.Consumer == null)
            {
                var consumerService = (IConsumerService)webApplication.Services.GetService(typeof(IConsumerService));
                consumerService.Consume();
                ConsumerHelper.Consumer = consumerService;
            }
        }
    }
}
