using MerlinClient.Properties;
using MerlinClient.Controller;
using MerlinClient.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MerlinClient.View
{
    class MenuViewController
    {
        private MerlinClientController controller;

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem[] ssModeItem;
        private MenuItem autoStartItem;
        private MenuItem aboutItem;
        private MenuItem exitItem;

        private BaseForm popForm;
        private LoginForm loginForm;
        private ClientForm clientForm;

        

        public MenuViewController(MerlinClientController controller)
        {
            this.controller = controller;
            notifyIcon = new NotifyIcon();
            LoadMenu();
            UpdateTrayIcon();
            notifyIcon.Visible = true;
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.MouseDoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseClick += notifyIcon_Click;

            this.controller.ShowPopForm += ShowPopForm;

        }

        ~MenuViewController()
        {
            notifyIcon.Dispose();
        }

        private MenuItem CreateMenuItem(string text, EventHandler click)
        {
            return new MenuItem(text, click);
        }

        private MenuItem CreateMenuGroup(string text, MenuItem[] items)
        {
            return new MenuItem(text, items);
        }

        private void LoadMenu()
        {
            this.ssModeItem = new MenuItem[5];
            this.contextMenu = new ContextMenu(new MenuItem[] {
                this.ssModeItem[0] = CreateMenuItem("SS禁用", new EventHandler(this.ssModeItem_Click)),
                this.ssModeItem[1] = CreateMenuItem("GFWlist模式", new EventHandler(this.ssModeItem_Click)),
                this.ssModeItem[2] = CreateMenuItem("大陆白名单模式", new EventHandler(this.ssModeItem_Click)),
                this.ssModeItem[3] = CreateMenuItem("游戏模式", new EventHandler(this.ssModeItem_Click)),
                this.ssModeItem[4] = CreateMenuItem("全局代理模式", new EventHandler(this.ssModeItem_Click)), 
                new MenuItem("-"),
                this.autoStartItem =  CreateMenuItem("开机自动运行", new EventHandler(this.autoStartItem_Click)),
                new MenuItem("-"),
                this.aboutItem =  CreateMenuItem("关于...", new EventHandler(this.aboutItem_Click)),
                new MenuItem("-"),
                this.exitItem =  CreateMenuItem("退出", new EventHandler(this.exitItem_Click))
            });
            
        }
        private void UpdateTrayIcon()
        {
            
            int dpi;
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            dpi = (int)graphics.DpiX;
            graphics.Dispose();

            Bitmap icon = null;
            if (dpi < 97)
            {
                icon = Resources.mc16;
            }
            else if (dpi < 121)
            {
                icon = Resources.mc20;
            }
            else
            {
                icon = Resources.mc24;
            }
            notifyIcon.Icon = Icon.FromHandle(icon.GetHicon());
                      
        }

        public void ShowBalloonTip(string title, string content, ToolTipIcon icon, int timeout)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = content;
            notifyIcon.BalloonTipIcon = icon;
            notifyIcon.ShowBalloonTip(timeout);
        }

        private void notifyIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowPopForm(this, null);
            }
        }

        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowPopForm(this , null);
            }
            if (e.Button == MouseButtons.Right)
            {
                //TODO
            }
        }

        void ShowPopForm(object sender, EventArgs e)
        {
            if (popForm != null)
                popForm.Activate();
            else
            {
                if (this.controller.IsLogin())
                {
                    popForm = clientForm = new ClientForm(this.controller);
                }
                else
                {
                    popForm = loginForm = new LoginForm(this.controller);
                }
                popForm.Show();
                popForm.Activate();
                popForm.FormClosed += PopForm_FormClosed;
            }
        }

        void PopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm = null;
            clientForm = null;
            popForm = null;
        }

        void autoStartItem_Click(object sender, EventArgs e)
        {
            this.autoStartItem.Checked = this.controller.autoStart(!this.autoStartItem.Checked);

        }

        void aboutItem_Click(object sender, EventArgs e)
        {
         
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        void exitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ssModeItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ssModeItem.Length; i++ )
            {
                if (ssModeItem[i] == (MenuItem)sender)
                {
                    this.controller.ChangeMode(i);
                    ssModeItem[i].Checked = true;
                }
                else
                {
                    ssModeItem[i].Checked = false;
                }

            }
        }
    }
}
