using MerlinClient.Controller;
using MerlinClient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MerlinClient.View
{
    public partial class LoginForm : MerlinClient.View.BaseForm
    {

        public LoginForm(MerlinClientController controller)
        {
            InitializeComponent();

            this.controller = controller;
            this.controller.Getway_Get += GetwayGet;
            GetwayGet(this, null);
            this.controller.getGetway();
        }

        void GetwayGet(object sender, EventArgs e)
        {
            this.labelIP.Text = this.controller.GetwayIP;
            this.labelMAC.Text = this.controller.GetwayMAC;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            this.controller.routerLogin(this.textPawword.Text);
            this.Close();
        }
    }
}
