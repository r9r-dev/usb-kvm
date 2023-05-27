using usb_kvm;

if (File.Exists("config.ini"))
{
    var config = Config.FromIni("config.ini");
    var usbEvents = new UsbEvents(config);
    usbEvents.Start();
}
else
{
    await File.AppendAllTextAsync("error.txt", $"[{DateTime.Now:s}] config.ini not found");
}

while (true)
{
    await Task.Delay(1000);
}
