public class Config
{
    public string UsbDeviceId { get; set; }
    public string MonitorName { get; set; }
    public string MonitorInputSelectVCPCode { get; set; }
    public string MonitorInputSelectDeviceInsertedValue { get; set; }
    public string MonitorInputSelectDeviceRemovedValue { get; set; }

    public string ControlMyMonitorExeFilePath { get; set; }

    public static Config FromIni(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var config = new Config();
        foreach (var line in lines)
        {
            var name = line.Split("=")[0].Trim().ToLower();
            var value = string.Join('=', line.Split("=").Skip(1)).Trim();

            switch (name)
            {
                case "usb_device_id":
                    config.UsbDeviceId = value;
                    break;
                case "monitor_name":
                    config.MonitorName = value;
                    break;
                case "monitor_input_select_vcp_code":
                    config.MonitorInputSelectVCPCode = value;
                    break;
                case "monitor_input_select_device_inserted_value":
                    config.MonitorInputSelectDeviceInsertedValue = value;
                    break;
                case "monitor_input_select_device_removed_value":
                    config.MonitorInputSelectDeviceRemovedValue = value;
                    break;
                case "control_my_monitor_path":
                    config.ControlMyMonitorExeFilePath = value;
                    break;
            }
        }

        return config;
    }
}