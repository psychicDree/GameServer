using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    internal class ControllerManager
    {
        private Dictionary<RequestCode,BaseController> controllerDict = new Dictionary<RequestCode,BaseController>();

        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            OnInit();
        }

        private void OnInit()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode,defaultController);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data,Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (!isGet)
            {
                Console.WriteLine("Cannot find controller for: " + requestCode);
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo methodInfo = controller.GetType().GetMethod(methodName);
            if (methodInfo == null)
            {
                Console.WriteLine("There is no corresponding method " + methodName);
                return;
            }

            object[] parameters = new object[] { data, client, server };
            object obj = methodInfo.Invoke(controller, parameters);
            if(obj == null)return;
            server.SendResponse(client, requestCode, obj as string);
        }
    }
}
