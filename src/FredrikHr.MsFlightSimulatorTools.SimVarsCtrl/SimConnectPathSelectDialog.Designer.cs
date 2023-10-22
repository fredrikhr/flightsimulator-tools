namespace FredrikHr.MsFlightSimulatorTools.SimVarsCtrl
{
    partial class SimConnectPathSelectDialog
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
            tableLayoutPanel1 = new TableLayoutPanel();
            sdkRadioButton = new RadioButton();
            separateRadioButton = new RadioButton();
            nativeLocationTextBox = new TextBox();
            nativeLocationBrowseButton = new Button();
            sdkLocationBrowseButton = new Button();
            nativeLocationLabel = new Label();
            managedLocationLabel = new Label();
            managedLocationTextBox = new TextBox();
            managedLocationBrowseButton = new Button();
            sdkLocationTextBox = new TextBox();
            buttonsPanel = new TableLayoutPanel();
            cancelButton = new Button();
            okButton = new Button();
            sdkFolderBrowserDialog = new FolderBrowserDialog();
            nativeFolderBrowserDialog = new FolderBrowserDialog();
            managedFolderBrowserDialog = new FolderBrowserDialog();
            tableLayoutPanel1.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(sdkRadioButton, 0, 0);
            tableLayoutPanel1.Controls.Add(separateRadioButton, 0, 2);
            tableLayoutPanel1.Controls.Add(nativeLocationTextBox, 2, 3);
            tableLayoutPanel1.Controls.Add(nativeLocationBrowseButton, 3, 3);
            tableLayoutPanel1.Controls.Add(sdkLocationBrowseButton, 3, 1);
            tableLayoutPanel1.Controls.Add(nativeLocationLabel, 1, 3);
            tableLayoutPanel1.Controls.Add(managedLocationLabel, 1, 4);
            tableLayoutPanel1.Controls.Add(managedLocationTextBox, 2, 4);
            tableLayoutPanel1.Controls.Add(managedLocationBrowseButton, 3, 4);
            tableLayoutPanel1.Controls.Add(sdkLocationTextBox, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(360, 142);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // sdkRadioButton
            // 
            sdkRadioButton.AutoSize = true;
            sdkRadioButton.Checked = true;
            tableLayoutPanel1.SetColumnSpan(sdkRadioButton, 4);
            sdkRadioButton.Location = new Point(3, 3);
            sdkRadioButton.Name = "sdkRadioButton";
            sdkRadioButton.Size = new Size(161, 19);
            sdkRadioButton.TabIndex = 0;
            sdkRadioButton.TabStop = true;
            sdkRadioButton.Text = "Select MSFS &SDK Location";
            sdkRadioButton.UseVisualStyleBackColor = true;
            sdkRadioButton.CheckedChanged += SdkRadioButton_CheckedChanged;
            // 
            // separateRadioButton
            // 
            separateRadioButton.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(separateRadioButton, 4);
            separateRadioButton.Location = new Point(3, 57);
            separateRadioButton.Name = "separateRadioButton";
            separateRadioButton.Size = new Size(330, 19);
            separateRadioButton.TabIndex = 1;
            separateRadioButton.Text = "Select SimConnect &Native && Managed Libraries separately";
            separateRadioButton.UseVisualStyleBackColor = true;
            separateRadioButton.CheckedChanged += SeparateRadioButton_CheckedChanged;
            // 
            // nativeLocationTextBox
            // 
            nativeLocationTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nativeLocationTextBox.Enabled = false;
            nativeLocationTextBox.Location = new Point(122, 82);
            nativeLocationTextBox.Name = "nativeLocationTextBox";
            nativeLocationTextBox.ReadOnly = true;
            nativeLocationTextBox.Size = new Size(154, 23);
            nativeLocationTextBox.TabIndex = 4;
            nativeLocationTextBox.TextChanged += PathLocationTextBox_TextChanged;
            // 
            // nativeLocationBrowseButton
            // 
            nativeLocationBrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nativeLocationBrowseButton.Enabled = false;
            nativeLocationBrowseButton.Location = new Point(282, 82);
            nativeLocationBrowseButton.Name = "nativeLocationBrowseButton";
            nativeLocationBrowseButton.Size = new Size(75, 23);
            nativeLocationBrowseButton.TabIndex = 5;
            nativeLocationBrowseButton.Text = "Browse...";
            nativeLocationBrowseButton.UseVisualStyleBackColor = true;
            nativeLocationBrowseButton.Click += NativeLocationBrowseButton_Click;
            // 
            // sdkLocationBrowseButton
            // 
            sdkLocationBrowseButton.Anchor = AnchorStyles.Right;
            sdkLocationBrowseButton.Location = new Point(282, 28);
            sdkLocationBrowseButton.Name = "sdkLocationBrowseButton";
            sdkLocationBrowseButton.Size = new Size(75, 23);
            sdkLocationBrowseButton.TabIndex = 2;
            sdkLocationBrowseButton.Text = "&Browse...";
            sdkLocationBrowseButton.UseVisualStyleBackColor = true;
            sdkLocationBrowseButton.Click += SdkLocationBrowseButton_Click;
            // 
            // nativeLocationLabel
            // 
            nativeLocationLabel.Anchor = AnchorStyles.Right;
            nativeLocationLabel.AutoSize = true;
            nativeLocationLabel.Location = new Point(39, 86);
            nativeLocationLabel.Name = "nativeLocationLabel";
            nativeLocationLabel.Size = new Size(77, 15);
            nativeLocationLabel.TabIndex = 6;
            nativeLocationLabel.Text = "Native library";
            // 
            // managedLocationLabel
            // 
            managedLocationLabel.Anchor = AnchorStyles.Right;
            managedLocationLabel.AutoSize = true;
            managedLocationLabel.Location = new Point(23, 115);
            managedLocationLabel.Name = "managedLocationLabel";
            managedLocationLabel.Size = new Size(93, 15);
            managedLocationLabel.TabIndex = 6;
            managedLocationLabel.Text = "Managed library";
            // 
            // managedLocationTextBox
            // 
            managedLocationTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            managedLocationTextBox.Enabled = false;
            managedLocationTextBox.Location = new Point(122, 111);
            managedLocationTextBox.Name = "managedLocationTextBox";
            managedLocationTextBox.ReadOnly = true;
            managedLocationTextBox.Size = new Size(154, 23);
            managedLocationTextBox.TabIndex = 4;
            managedLocationTextBox.TextChanged += PathLocationTextBox_TextChanged;
            // 
            // managedLocationBrowseButton
            // 
            managedLocationBrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            managedLocationBrowseButton.Enabled = false;
            managedLocationBrowseButton.Location = new Point(282, 111);
            managedLocationBrowseButton.Name = "managedLocationBrowseButton";
            managedLocationBrowseButton.Size = new Size(75, 23);
            managedLocationBrowseButton.TabIndex = 5;
            managedLocationBrowseButton.Text = "Browse...";
            managedLocationBrowseButton.UseVisualStyleBackColor = true;
            managedLocationBrowseButton.Click += ManagedLocationBrowseButton_Click;
            // 
            // sdkLocationTextBox
            // 
            sdkLocationTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(sdkLocationTextBox, 2);
            sdkLocationTextBox.Location = new Point(23, 28);
            sdkLocationTextBox.Name = "sdkLocationTextBox";
            sdkLocationTextBox.Size = new Size(253, 23);
            sdkLocationTextBox.TabIndex = 3;
            sdkLocationTextBox.TextChanged += PathLocationTextBox_TextChanged;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonsPanel.AutoSize = true;
            buttonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonsPanel.ColumnCount = 2;
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.Controls.Add(cancelButton, 1, 0);
            buttonsPanel.Controls.Add(okButton, 0, 0);
            buttonsPanel.Location = new Point(210, 160);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.RowCount = 1;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonsPanel.Size = new Size(162, 29);
            buttonsPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(84, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "&Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Enabled = false;
            okButton.Location = new Point(3, 3);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "&OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // sdkFolderBrowserDialog
            // 
            sdkFolderBrowserDialog.InitialDirectory = "C:\\MSFS SDK";
            // 
            // SimConnectPathSelectDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(384, 201);
            Controls.Add(buttonsPanel);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(400, 240);
            Name = "SimConnectPathSelectDialog";
            Text = "Select SimConnect directories";
            FormClosing += DialogClosing;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel buttonsPanel;
        private Button cancelButton;
        private Button okButton;
        private RadioButton sdkRadioButton;
        private RadioButton separateRadioButton;
        private Button sdkLocationBrowseButton;
        private TextBox sdkLocationTextBox;
        private TextBox nativeLocationTextBox;
        private Button nativeLocationBrowseButton;
        private Label nativeLocationLabel;
        private Label managedLocationLabel;
        private TextBox managedLocationTextBox;
        private Button managedLocationBrowseButton;
        private FolderBrowserDialog sdkFolderBrowserDialog;
        private FolderBrowserDialog nativeFolderBrowserDialog;
        private FolderBrowserDialog managedFolderBrowserDialog;
    }
}
