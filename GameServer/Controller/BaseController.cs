using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Common;

namespace GameServer.Controller
{
    abstract class BaseController
    {
        protected RequestCode requestCode = RequestCode.None;
        protected ActionCode actionCode = ActionCode.None;

        public RequestCode RequestCode
        {
            get
            {
                return requestCode;
            }
        }

    }
}
