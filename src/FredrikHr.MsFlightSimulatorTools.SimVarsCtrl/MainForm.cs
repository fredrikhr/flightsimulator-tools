using FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

namespace FredrikHr.MsFlightSimulatorTools.SimVarsCtrl;

public partial class MainForm : Form
{
    const int WM_USER_SIMCONNECT = 0x0402;

    public MainForm()
    {
        InitializeComponent();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types")]
    private void MainForm_Load(object sender, EventArgs e)
    {
        DialogResult loadFromEnvironmentVariable = DialogResult.Yes;
#if DEBUG
        loadFromEnvironmentVariable = MessageBox.Show(
            text: "Attempt to load SimConnect libraries from environment varaible?",
            caption: "SimConnect Path",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1
            );
#endif
        SimConnectAssemblyPath? simConnectPath = null;
        if (loadFromEnvironmentVariable == DialogResult.Yes)
        {
            try
            {
                simConnectPath = SimConnectAssemblyPath.FromMsFsSdkEnvironmentVariable();
            }
            catch (Exception)
            {
                simConnectPath = null;
            }
        }
        InitializeSimConnectApplication(simConnectPath);
    }

    private void InitializeSimConnectApplication(SimConnectAssemblyPath? simConnectPath)
    {
        if (simConnectPath is null)
        {
            using SimConnectPathSelectDialog pathSelectDialog = new();
            var pathSelectResult = pathSelectDialog.ShowDialog();
            simConnectPath = pathSelectDialog.SimConnectPath;
        }

        try
        {
            
        }
        catch (Exception)
        {

            throw;
        }
    }
}
