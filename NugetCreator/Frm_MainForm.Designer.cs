namespace NugetTest
{
    partial class Frm_MainForm
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.frtxtOutputDirectory = new DevExpress.XtraEditors.TextEdit();
            this.frtxtSlnPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.frbitPrerelease = new DevExpress.XtraEditors.CheckEdit();
            this.frtxtPreleaseNumber = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtOutputDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtSlnPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frbitPrerelease.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtPreleaseNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(706, 70);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(114, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Dokonaj wersję!";
            this.simpleButton1.Click += new System.EventHandler(this.ReleaseButtonClick);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoEdit1.Location = new System.Drawing.Point(12, 137);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(808, 272);
            this.memoEdit1.TabIndex = 1;
            this.memoEdit1.UseOptimizedRendering = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 47);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Folder wyjściowy:";
            // 
            // frtxtOutputDirectory
            // 
            this.frtxtOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frtxtOutputDirectory.EditValue = "\\\\192.168.255.21\\Wspólny\\NuGet";
            this.frtxtOutputDirectory.Location = new System.Drawing.Point(108, 44);
            this.frtxtOutputDirectory.Name = "frtxtOutputDirectory";
            this.frtxtOutputDirectory.Size = new System.Drawing.Size(712, 20);
            this.frtxtOutputDirectory.TabIndex = 3;
            // 
            // frtxtSlnPath
            // 
            this.frtxtSlnPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frtxtSlnPath.EditValue = "D:\\Praca\\inSolutions\\Projekty\\SzkieletAplikacji\\ApplicationFrame-Evaluation\\Appli" +
    "cationFrame.sln";
            this.frtxtSlnPath.Location = new System.Drawing.Point(108, 9);
            this.frtxtSlnPath.Name = "frtxtSlnPath";
            this.frtxtSlnPath.Size = new System.Drawing.Size(712, 20);
            this.frtxtSlnPath.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Solucja wejściowa:";
            // 
            // frbitPrerelease
            // 
            this.frbitPrerelease.EditValue = true;
            this.frbitPrerelease.Location = new System.Drawing.Point(12, 107);
            this.frbitPrerelease.Name = "frbitPrerelease";
            this.frbitPrerelease.Properties.Caption = "Prerelase";
            this.frbitPrerelease.Size = new System.Drawing.Size(69, 19);
            this.frbitPrerelease.TabIndex = 6;
            this.frbitPrerelease.CheckedChanged += new System.EventHandler(this.frbitPrerelease_CheckedChanged);
            // 
            // frtxtPreleaseNumber
            // 
            this.frtxtPreleaseNumber.EditValue = "alpha";
            this.frtxtPreleaseNumber.Location = new System.Drawing.Point(87, 106);
            this.frtxtPreleaseNumber.Name = "frtxtPreleaseNumber";
            this.frtxtPreleaseNumber.Properties.MaxLength = 13;
            this.frtxtPreleaseNumber.Size = new System.Drawing.Size(168, 20);
            this.frtxtPreleaseNumber.TabIndex = 8;
            // 
            // Frm_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 417);
            this.Controls.Add(this.frtxtPreleaseNumber);
            this.Controls.Add(this.frbitPrerelease);
            this.Controls.Add(this.frtxtSlnPath);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.frtxtOutputDirectory);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.simpleButton1);
            this.MinimumSize = new System.Drawing.Size(448, 456);
            this.Name = "Frm_MainForm";
            this.Text = "NuGet Generator";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtOutputDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtSlnPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frbitPrerelease.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtPreleaseNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit frtxtOutputDirectory;
        private DevExpress.XtraEditors.TextEdit frtxtSlnPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit frbitPrerelease;
        private DevExpress.XtraEditors.TextEdit frtxtPreleaseNumber;
    }
}

