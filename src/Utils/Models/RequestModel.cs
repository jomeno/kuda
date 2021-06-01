using System;

namespace Kuda.Logistics.Models
{
    public class RequestModel
    {
        public string ServiceType { get; set; }
        public string RequestRef { get; set; }
        public RequestData Data { get; set; }

        public RequestModel()
        {
            RequestRef = DateTime.UtcNow.Ticks.ToString();//DateTime.Now.ToString("MMddyyyyhmmss");
            Data = new RequestData();
        }

        public RequestModel(string serviceType) : this()
        {
            ServiceType = serviceType;
        }
    }

    public class RequestData
    {
        public string Param { get; set; }
    }

}