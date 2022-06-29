using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbModel.MongoDb
{
    public class RequestMessageModel
    {
        public string RequestId { get; set; }
        public string MessageJson { get; set; }
        public string WorkerId { get; set; }
    }
}
