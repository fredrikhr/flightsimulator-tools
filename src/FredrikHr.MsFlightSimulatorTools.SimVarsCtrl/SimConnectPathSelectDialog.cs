using FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

namespace FredrikHr.MsFlightSimulatorTools.SimVarsCtrl;

public partial class SimConnectPathSelectDialog : Form
{
    public SimConnectAssemblyPath SimConnectPath { get; private set; } = null!;

    public SimConnectPathSelectDialog()
    {
        InitializeComponent();
    }

    private void SdkRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (!sdkRadioButton.Checked) return;
        EnsureControlMode(separatePaths: false);
    }

    private void SeparateRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (!separateRadioButton.Checked) return;
        EnsureControlMode(separatePaths: true);
    }

    private void EnsureControlMode(bool separatePaths)
    {
        sdkLocationTextBox.Enabled = !separatePaths;
        sdkLocationTextBox.ReadOnly = separatePaths;
        sdkLocationBrowseButton.Enabled = !separatePaths;

        nativeLocationTextBox.Enabled = separatePaths;
        nativeLocationTextBox.ReadOnly = !separatePaths;
        nativeLocationBrowseButton.Enabled = separatePaths;

        managedLocationTextBox.Enabled = separatePaths;
        managedLocationTextBox.ReadOnly = !separatePaths;
        managedLocationBrowseButton.Enabled = separatePaths;
    }

    private void SdkLocationBrowseButton_Click(object sender, EventArgs e)
    {
        if (sdkFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            sdkLocationTextBox.Text = sdkFolderBrowserDialog.SelectedPath;
    }

    private void NativeLocationBrowseButton_Click(object sender, EventArgs e)
    {
        if (nativeFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            nativeLocationTextBox.Text = nativeFolderBrowserDialog.SelectedPath;
    }

    private void ManagedLocationBrowseButton_Click(object sender, EventArgs e)
    {
        if (managedFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            managedLocationTextBox.Text = managedFolderBrowserDialog.SelectedPath;
    }

    private void PathLocationTextBox_TextChanged(object sender, EventArgs e) =>
        ValidatePathSelected();

    private void ValidatePathSelected()
    {
        if (sdkRadioButton.Checked)
        {
            string sdkLocationPath = sdkLocationTextBox.Text;
            if (!string.IsNullOrEmpty(sdkLocationPath) &&
                Directory.Exists(sdkLocationPath))
            {
                EnsureDialogAcceptCondition(condition: true);
                return;
            }
        }
        else if (separateRadioButton.Checked)
        {
            string nativeLocationPath = nativeLocationTextBox.Text;
            string managedLocationPath = managedLocationTextBox.Text;
            if (!string.IsNullOrEmpty(nativeLocationPath) &&
                Directory.Exists(nativeLocationPath) &&
                !string.IsNullOrEmpty(managedLocationPath) &&
                Directory.Exists(managedLocationPath))
            {
                EnsureDialogAcceptCondition(condition: true);
                return;
            }
        }

        EnsureDialogAcceptCondition(condition: false);
    }

    private void EnsureDialogAcceptCondition(bool condition)
    {
        okButton.Enabled = condition;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031: Do not catch general exception types")]
    private void DialogClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
        {
            try
            {
                if (sdkRadioButton.Checked)
                {
                    string sdkLocationPath = sdkLocationTextBox.Text;
                    SimConnectPath = SimConnectAssemblyPath.FromMsFsSdkPath(sdkLocationPath);
                }
                else if (separateRadioButton.Checked)
                {
                    string nativeLocationPath = nativeLocationTextBox.Text;
                    string managedLocationPath = managedLocationTextBox.Text;
                    SimConnectPath = new(nativeLocationPath, managedLocationPath);
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString(), "SimConnect Paths", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SimConnectPath = null!;
                e.Cancel = true;
            }
        }

        SimConnectPath = null!;
    }
}
