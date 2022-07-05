using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {
        public string ErrorMessage;
        public object ReturnValue;
       
        public Response()
        {
            this.ErrorMessage = null;
            this.ReturnValue = null;
        }

         public Response(string msg, Object ReturnValue)
        {
            this.ErrorMessage = msg;
            this.ReturnValue = ReturnValue;
        }
    }
}
