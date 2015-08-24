using MerlinClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerlinClient.Controller
{
    public class MerlinClientController
    {

        public event EventHandler ShowPopForm;
        public event EventHandler Getway_Get;

        public string GetwayIP;
        public string GetwayMAC;

        public int RouterPort = 80;

        private Router router;


        public void start()
        {
            ShowPopForm(this, null);

        }

        public void getGetway()
        {
            if(!String.IsNullOrEmpty(GetwayIP = BaseNetwork.GetGateway()))
            {
                GetwayMAC = BaseNetwork.GetMacAddress(GetwayIP);
            }
            Getway_Get(this, null);
        }

        public void routerLogin(string password)
        {
            router = new Router(GetwayIP, GetwayMAC, RouterPort, password);
            router.Login();

        }

        public bool IsLogin()
        {
            if (router == null)
                return false;
            return router.IsLogin();

        }

        public bool ChangeMode(int mode)
        {
            if (router == null)
                return false;
            return router.ChangeMode(mode);
        }

        public bool autoStart(bool enabled)
        {
            AutoStartup.Set(enabled);
            return AutoStartup.Check();
        }
    }
}
