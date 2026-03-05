import win32com.client
import pythoncom

print("Monitoring USB plug/unplug events...\n")

pythoncom.CoInitialize()

wmi = win32com.client.GetObject("winmgmts:")

# Watch for creation (plug in)
creation_query = """
SELECT * FROM __InstanceCreationEvent WITHIN 2
WHERE TargetInstance ISA 'Win32_PnPEntity'
"""

# Watch for deletion (unplug)
deletion_query = """
SELECT * FROM __InstanceDeletionEvent WITHIN 2
WHERE TargetInstance ISA 'Win32_PnPEntity'
"""

creation_watcher = wmi.ExecNotificationQuery(creation_query)
deletion_watcher = wmi.ExecNotificationQuery(deletion_query)

while True:
    try:
        # Plugged in
        try:
            event = creation_watcher.NextEvent(1000)
            device = event.TargetInstance

            if "USB" in device.DeviceID:
                print("\nUSB Device Plugged In:")
                print("Name:", device.Name)
                print("DeviceID:", device.DeviceID)

        except pythoncom.com_error:
            pass

        # Unplugged
        try:
            event = deletion_watcher.NextEvent(1000)
            device = event.TargetInstance

            if "USB" in device.DeviceID:
                print("\nUSB Device Unplugged:")
                print("Name:", device.Name)
                print("DeviceID:", device.DeviceID)

        except pythoncom.com_error:
            pass

    except KeyboardInterrupt:
        print("\nStopped monitoring.")
        break