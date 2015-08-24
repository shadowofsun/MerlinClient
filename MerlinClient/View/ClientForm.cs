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
    public partial class ClientForm : MerlinClient.View.BaseForm
    {
        public ClientForm(MerlinClientController controller)
        {
            InitializeComponent();
        }
    }
}
