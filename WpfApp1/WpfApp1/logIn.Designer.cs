namespace WpfApp1
{
    partial class logIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.logInButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.passwordText = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(18, 27);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(64, 13);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "User name: ";
            this.usernameLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(18, 83);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password: ";
            this.passwordLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // usernameText
            // 
            this.usernameText.Location = new System.Drawing.Point(86, 24);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(95, 20);
            this.usernameText.TabIndex = 3;
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(21, 138);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(70, 40);
            this.logInButton.TabIndex = 5;
            this.logInButton.Text = "Log-in";
            this.logInButton.UseVisualStyleBackColor = true;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(111, 138);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(70, 40);
            this.registerButton.TabIndex = 6;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            // 
            // passwordText
            // 
            this.passwordText.Location = new System.Drawing.Point(86, 80);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(95, 20);
            this.passwordText.TabIndex = 7;
            // 
            // logIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 199);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Name = "logIn";
            this.Text = "Log-In";
            this.Load += new System.EventHandler(this.logIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.MaskedTextBox passwordText;
    }
}