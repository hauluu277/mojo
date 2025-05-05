using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.Service.CommonBusiness
{
    public class JsonResultBO
    {
        public object Data { get; set; }
        private bool status { get; set; }
        private string message { get; set; }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public JsonResultBO(bool status)
        {
            this.status = status;
        }

        public JsonResultBO(bool status, string message)
        {
            this.status = status;
            this.message = message;
        }


        public void MessageFail(string message)
        {
            this.status = false;
            this.message = message;
        }
    }
}
