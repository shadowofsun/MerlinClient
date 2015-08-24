using MerlinClient.Controller;
using MerlinClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MerlinClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MerlinClientController controller = new MerlinClientController();
            MenuViewController viewController = new MenuViewController(controller);
            controller.start();
            Application.Run();
        }
    }
}
