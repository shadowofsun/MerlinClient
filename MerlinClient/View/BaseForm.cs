using MerlinClient.Controller;
using MerlinClient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MerlinClient.View
{
    public partial class BaseForm : Form
    {
        [DllImport("user32.dll")] 
        public static extern int SetWindowLong(int hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        protected static extern int GetWindowLong(int hwindow, int unindex);
        long WS_BORDER = 0x00C00000L;
        int GWL_STYLE = (-16);

        public MerlinClientController controller;
        
        public BaseForm()
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = false;
            SetWindowLong((int)this.Handle, GWL_STYLE, (int)(GetWindowLong((int)this.Handle, GWL_STYLE) & ~WS_BORDER));
            this.ClientSize = new System.Drawing.Size(350, 500);
            this.BackColor = System.Drawing.Color.White;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Bounds.Width - 8, Screen.PrimaryScreen.WorkingArea.Bottom - this.Bounds.Height - 8);
            this.Deactivate += new System.EventHandler(this.BaseForm_Deactivate);
        }

        private void BaseForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
