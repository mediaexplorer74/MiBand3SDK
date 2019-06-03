using MiBand3SDK.Components;
using MiBand3SDK.Utils;
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace MiBand3SDK
{
    public class MiBand3
    {        
        public MiBand3()
        {
            Identity = new Identity();
            Battery = new Battery();
            Device = new Device();
            Display = new Display();
            Activity = new Activity();
            HeartRate = new HeartRate();
            WearLocation = new WearLocation();
            Notifications = new Notification();
        }
        
        public Identity Identity { get; }
        public Battery Battery { get; }
        public Device Device { get; }
        public Display Display { get; }
        public Activity Activity { get; }
        public HeartRate HeartRate { get; }
        public WearLocation WearLocation { get; }
        public Notification Notifications { get; }
        
        /// <summary>
        /// Connect to paired device
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ConnectAsync()
        {
            Identity auth = new Identity();
            DeviceInformation device = await auth.GetPairedBand();

            if (device != null)
            {
                Gatt.bluetoothLEDevice = await BluetoothLEDevice.FromIdAsync(device.Id);
                return Gatt.bluetoothLEDevice != null;
            }

            return false;
        }

        /// <summary>
        /// Connect to device by id
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public async Task<bool> ConnectAsync(DeviceInformation deviceInfo)
        {
            if (deviceInfo != null)
            {
                Gatt.bluetoothLEDevice = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
                return Gatt.bluetoothLEDevice != null;
            }

            return false;
        }

        /// <summary>
        /// Unpair band from device
        /// </summary>
        public async Task UnpairDeviceAsync()
        {
            if (Gatt.bluetoothLEDevice != null)
                await Gatt.bluetoothLEDevice.DeviceInformation.Pairing.UnpairAsync();
        }

        public bool IsConnected() => Gatt.bluetoothLEDevice != null && Gatt.bluetoothLEDevice.ConnectionStatus == BluetoothConnectionStatus.Connected;
    }
}
