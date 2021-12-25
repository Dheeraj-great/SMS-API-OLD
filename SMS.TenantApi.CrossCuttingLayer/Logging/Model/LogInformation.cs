using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.CrossCuttingLayer.Logging.Model
{
    public class LogInformation
    {
        public Guid UserGuid { get; set; }
        public string Message { get; set; }
        public string Module { get; set; }
        public string Data { get; set; }
        public Exception Exception { get; set; }
    }
}
