using System.Configuration;


namespace GoogleVoice.NET
{
    partial class FormControls
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControls));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSignOut = new System.Windows.Forms.Label();
            this.timerCancelCall = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TestTab = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.recievedSMSTextBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contactsLookupButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.sendTestSMSButton = new System.Windows.Forms.Button();
            this.testSMSMessage = new System.Windows.Forms.ComboBox();
            this.sentListView = new System.Windows.Forms.ListView();
            this.contactsSelectionDropdown = new System.Windows.Forms.ComboBox();
            this.recipientNumberBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TestTab.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(8, 9);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(879, 64);
            this.panel3.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSignOut);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(679, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 64);
            this.panel1.TabIndex = 0;
            // 
            // lblSignOut
            // 
            this.lblSignOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSignOut.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSignOut.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSignOut.Location = new System.Drawing.Point(127, 4);
            this.lblSignOut.Name = "lblSignOut";
            this.lblSignOut.Size = new System.Drawing.Size(56, 17);
            this.lblSignOut.TabIndex = 10;
            this.lblSignOut.Text = "Sign out";
            this.lblSignOut.Click += new System.EventHandler(this.lblSignOut_Click);
            // 
            // timerCancelCall
            // 
            this.timerCancelCall.Interval = 30000;
            this.timerCancelCall.Tick += new System.EventHandler(this.timerCancelCall_Tick);
            // 
            // TestTab
            // 
            this.TestTab.Controls.Add(this.tabPage3);
            this.TestTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestTab.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestTab.Location = new System.Drawing.Point(8, 73);
            this.TestTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TestTab.Name = "TestTab";
            this.TestTab.SelectedIndex = 0;
            this.TestTab.Size = new System.Drawing.Size(879, 459);
            this.TestTab.TabIndex = 99;
            this.TestTab.Text = "Test";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.recievedSMSTextBox);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.contactsLookupButton);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.sendTestSMSButton);
            this.tabPage3.Controls.Add(this.testSMSMessage);
            this.tabPage3.Controls.Add(this.sentListView);
            this.tabPage3.Controls.Add(this.contactsSelectionDropdown);
            this.tabPage3.Controls.Add(this.recipientNumberBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(871, 433);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "Test";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar1.Location = new System.Drawing.Point(169, 401);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(520, 23);
            this.progressBar1.TabIndex = 32;
            this.progressBar1.UseWaitCursor = true;
            // 
            // recievedSMSTextBox
            // 
            this.recievedSMSTextBox.Location = new System.Drawing.Point(172, 120);
            this.recievedSMSTextBox.Multiline = true;
            this.recievedSMSTextBox.Name = "recievedSMSTextBox";
            this.recievedSMSTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.recievedSMSTextBox.Size = new System.Drawing.Size(685, 274);
            this.recievedSMSTextBox.TabIndex = 31;
            this.recievedSMSTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(695, 400);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 23);
            this.button3.TabIndex = 30;
            this.button3.Text = "Export Results";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Test All SMS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.run_Test_Messages);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 28;
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(169, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 22);
            this.textBox1.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(690, 48);
            this.button1.TabIndex = 26;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // contactsLookupButton
            // 
            this.contactsLookupButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.contactsLookupButton.AutoCheck = false;
            this.contactsLookupButton.Location = new System.Drawing.Point(753, 13);
            this.contactsLookupButton.Name = "contactsLookupButton";
            this.contactsLookupButton.Size = new System.Drawing.Size(105, 46);
            this.contactsLookupButton.TabIndex = 24;
            this.contactsLookupButton.Text = "Contacts...";
            this.contactsLookupButton.UseVisualStyleBackColor = true;
            this.contactsLookupButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.contactsLookupButton.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 22;
            // 
            // sendTestSMSButton
            // 
            this.sendTestSMSButton.Location = new System.Drawing.Point(10, 65);
            this.sendTestSMSButton.Name = "sendTestSMSButton";
            this.sendTestSMSButton.Size = new System.Drawing.Size(152, 48);
            this.sendTestSMSButton.TabIndex = 4;
            this.sendTestSMSButton.Text = "Send";
            this.sendTestSMSButton.UseVisualStyleBackColor = true;
            this.sendTestSMSButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // testSMSMessage
            // 
            this.testSMSMessage.FormattingEnabled = true;
            this.testSMSMessage.Items.AddRange(new object[] {
            ".help",
            ".info",
            ".info help",
            ".info cell",
            ".info gps",
            ".info fw",
            ".info vcore",
            ".info ign",
            ".info obd",
            ".info battery",
            ".info sms",
            ".info wifi",
            ".info failure",
            ".info <invalid>",
            ".info testing",
            ".info <blank>",
            ".packageList",
            ".toolbelt",
            ".status",
            ".info c",
            ".status c",
            ".devhelp",
            ".getlogs",
            ".getlogs (<appDirectory ! all> (ftpHost(:port) userName password))",
            ".reboot",
            ".setapn <apn>",
            ".setapn <invalid>",
            ".setapn <blank>",
            ".sh <scriptName> <parameters>",
            ".sh <invalid>",
            ".sh <blank>"});
            this.testSMSMessage.Location = new System.Drawing.Point(11, 27);
            this.testSMSMessage.Name = "testSMSMessage";
            this.testSMSMessage.Size = new System.Drawing.Size(152, 21);
            this.testSMSMessage.TabIndex = 3;
            this.testSMSMessage.SelectedIndexChanged += new System.EventHandler(this.testSMSMessage_SelectedIndexChanged);
            // 
            // sentListView
            // 
            this.sentListView.Location = new System.Drawing.Point(10, 119);
            this.sentListView.Name = "sentListView";
            this.sentListView.Size = new System.Drawing.Size(152, 274);
            this.sentListView.TabIndex = 1;
            this.sentListView.UseCompatibleStateImageBehavior = false;
            this.sentListView.View = System.Windows.Forms.View.List;
            // 
            // contactsSelectionDropdown
            // 
            this.contactsSelectionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.contactsSelectionDropdown.FormattingEnabled = true;
            this.contactsSelectionDropdown.Location = new System.Drawing.Point(463, 27);
            this.contactsSelectionDropdown.Name = "contactsSelectionDropdown";
            this.contactsSelectionDropdown.Size = new System.Drawing.Size(239, 21);
            this.contactsSelectionDropdown.Sorted = true;
            this.contactsSelectionDropdown.TabIndex = 23;
            this.contactsSelectionDropdown.Visible = false;
            this.contactsSelectionDropdown.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // recipientNumberBox
            // 
            this.recipientNumberBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.recipientNumberBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.recipientNumberBox.Location = new System.Drawing.Point(463, 27);
            this.recipientNumberBox.Name = "recipientNumberBox";
            this.recipientNumberBox.Size = new System.Drawing.Size(239, 22);
            this.recipientNumberBox.TabIndex = 25;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GoogleVoice.NET.Properties.Resources.Google_Voice_Small;
            this.pictureBox1.Location = new System.Drawing.Point(1, -6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(895, 541);
            this.Controls.Add(this.TestTab);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormControls";
            this.Padding = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GoogleVoice.NET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControls_FormClosing);
            this.Load += new System.EventHandler(this.FormControls_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.TestTab.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSignOut;
        private System.Windows.Forms.Timer timerCancelCall;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl TestTab;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton contactsLookupButton;
        private System.Windows.Forms.ComboBox contactsSelectionDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendTestSMSButton;
        private System.Windows.Forms.ComboBox testSMSMessage;
        private System.Windows.Forms.ListView sentListView;
        private System.Windows.Forms.TextBox recipientNumberBox;
        private System.Windows.Forms.TextBox recievedSMSTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}