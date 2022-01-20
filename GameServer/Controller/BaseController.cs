﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Controller
{
    abstract class BaseController
    {
        private RequestCode requestCode = RequestCode.None;
        private ActionCode actionCode = ActionCode.None;

        public RequestCode RequestCode
        {
            get
            {
                return requestCode;
            }
        }

    }
}
