# USB-KVM

USB-KVM is a tool to switch between computers using a single monitor without the need to use a KVM switch.

## Why USB-KVM ?

A KVM switch with DisplayPorts is expensive and you can have some issues if you need high resolution and high refresh rate. I have a laptop with HDMI and a desktop with DisplayPort and it can be even more expensive to find a KVM switch with DisplayPort and HDMI. Having my computers directly connected to my monitor will always be better.

USB switchs are far less expensive, bought mine for 30€. So why not use it to also switch the monitor ?

## How it works

USB-KVM monitor a specific USB device. When it is inserted or removed, it will switch the monitor input. If you monitor your keyboard or mouse, switching them will connect or disconnect them from your computer. So when you switch your keyboard/mouse, you also switch your monitor.

## Requirements

- At least 1 computer with Windows
- ControlMyMonitor from NirSoft : https://www.nirsoft.net/utils/control_my_monitor.html
- A monitor with at least 2 inputs (mine are DisplayPort and HDMI)
- A USB switch to switch keyboard/mouse

## How to use

1. Run ControlMyMonitor and find the monitor you want to switch. In my case, it is the first one so I will use `Primary`.
2. On ControlMyMonitor, search for `Input Select` and note the VCP Code. In my case, it is `60`.
3. Do some tests to find the value for each input. In my case, `15` is DisplayPort and `17` is HDMI. You can write the value in ControlMyMonitor to test it.
4. Find your USB Device Id. Go to Device Manager, find your device, right click on it and select `Properties`. Go to `Details` and select `Device instance path`. Note the value. In my case, it is `HID\VID_1050&PID_0402\A&2E2972A3&0&0000`.
5. edit `config.ini` and set all the required values.

## How to run

Run `usb-kvm.exe`. You can also add it to your startup programs.
