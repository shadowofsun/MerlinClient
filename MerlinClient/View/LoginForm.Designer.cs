namespace MerlinClient.View
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textPawword = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelMAC = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textPawword
            // 
            this.textPawword.Location = new System.Drawing.Point(66, 217);
            this.textPawword.Name = "textPawword";
            this.textPawword.PasswordChar = '*';
            this.textPawword.Size = new System.Drawing.Size(209, 21);
            this.textPawword.TabIndex = 0;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(97, 90);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(0, 12);
            this.labelIP.TabIndex = 1;
            // 
            // labelMAC
            // 
            this.labelMAC.AutoSize = true;
            this.labelMAC.Location = new System.Drawing.Point(99, 135);
            this.labelMAC.Name = "labelMAC";
            this.labelMAC.Size = new System.Drawing.Size(0, 12);
            this.labelMAC.TabIndex = 2;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(131, 322);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 3;
            this.buttonSubmit.Text = "登录";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonSubmit;
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.labelMAC);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textPawword);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPawword;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelMAC;
        private System.Windows.Forms.Button buttonSubmit;
    }
}
