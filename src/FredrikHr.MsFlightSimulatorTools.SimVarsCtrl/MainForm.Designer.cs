namespace FredrikHr.MsFlightSimulatorTools.SimVarsCtrl;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        statusStrip = new StatusStrip();
        simconnectConnectionStatusLabel = new ToolStripStatusLabel();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { simconnectConnectionStatusLabel });
        statusStrip.Location = new Point(0, 428);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(800, 22);
        statusStrip.TabIndex = 0;
        statusStrip.Text = "statusStrip";
        // 
        // simconnectConnectionStatusLabel
        // 
        simconnectConnectionStatusLabel.Name = "simconnectConnectionStatusLabel";
        simconnectConnectionStatusLabel.Size = new Size(168, 17);
        simconnectConnectionStatusLabel.Text = "Not connected to SimConnect";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(statusStrip);
        Name = "MainForm";
        Text = "SimVars Control";
        Load += MainForm_Load;
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private StatusStrip statusStrip;
    private ToolStripStatusLabel simconnectConnectionStatusLabel;
}