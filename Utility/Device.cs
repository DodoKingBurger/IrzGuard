using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace IrzGuard.Utility
{
    /// <summary>
    /// Устройство.
    /// </summary>
    public static class DeviceSystem
    {
        /// <summary>
        /// Получения кода устройства взависимости от платформы. 
        /// </summary>
        /// <returns>Код устройства.</returns>
        static public string GetCodeDevice()
        {
            string deviceID = "0000 0000 0000 0000";
#if ANDROID
        deviceID = Android.Provider.Settings.Secure.GetString(Platform.CurrentActivity.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
#elif IOS
            deviceID = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.ToString();
#elif WINDOWS
        deviceID = NetworkInterface.GetAllNetworkInterfaces()
                                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                .Select(nic => nic.GetPhysicalAddress().ToString())
                                .FirstOrDefault();
#endif
            return deviceID;
        }
    }
}
