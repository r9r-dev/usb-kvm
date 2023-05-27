using System.Management;
using System.Runtime.Versioning;

namespace usb_kvm;

[SupportedOSPlatform("windows")]
public class UsbEvents
{
    private readonly ManagementEventWatcher _insertWatcher;
    private readonly ManagementEventWatcher _removeWatcher;

    private readonly Config _config;

    public UsbEvents(Config config)
    {
        _config = config;

        var insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity'");
        _insertWatcher = new ManagementEventWatcher(insertQuery);
        _insertWatcher.EventArrived += DeviceInsertedEvent;

        var removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity'");
        _removeWatcher = new ManagementEventWatcher(removeQuery);
        _removeWatcher.EventArrived += DeviceRemovedEvent;
    }

    public void Start()
    {
        _insertWatcher.Start();
        _removeWatcher.Start();
    }

    private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
    {
        var instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
        if (!instance.Properties["DeviceID"].Value.ToString()!
                .Contains(_config.UsbDeviceId)) return;
        var process = new System.Diagnostics.Process();
        process.StartInfo.FileName = _config.ControlMyMonitorExeFilePath;
        process.StartInfo.Arguments = $"/SetValue {_config.MonitorName} {_config.MonitorInputSelectVCPCode} {_config.MonitorInputSelectDeviceInsertedValue}";
        process.Start();
    }

    private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
    {
        var instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
        if (!instance.Properties["DeviceID"].Value.ToString()!
                .Contains(_config.UsbDeviceId)) return;
        var process = new System.Diagnostics.Process();
        process.StartInfo.FileName = _config.ControlMyMonitorExeFilePath;
        process.StartInfo.Arguments = $"/SetValue {_config.MonitorName} {_config.MonitorInputSelectVCPCode} {_config.MonitorInputSelectDeviceRemovedValue}";
        process.Start();
    }
}