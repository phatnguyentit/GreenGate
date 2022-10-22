using System;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace GreenGate.License
{
    public class LicenseManager
    {
        internal bool CheckAppLicense()
        {
            try
            {
                var mac = GetMacAddress();
                var binary = GetLicenseBinary();
                if (!string.IsNullOrWhiteSpace(mac?.ToString()) && !string.IsNullOrWhiteSpace(binary))
                {
                    var httpClient = new HttpClient();

                    // TODO Make this ip configurable
                    httpClient.GetAsync($"http://103.1.236.36/license/greengate?mac={mac}&binary={binary}");
                }
                
                return CheckIronOcrLicense();
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Obsolete]
        private bool CheckIronOcrLicense()
        {
            var licenceKey = System.Configuration.ConfigurationManager.AppSettings["IronOcr.LicenseKey"];
            bool result = IronOcr.License.IsValidLicense(licenceKey);
            if (result)
            {
                IronOcr.Installation.LicenseKey = licenceKey;
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetLicenseBinary()
        {
            var binPath = Path.Combine(Directory.GetCurrentDirectory(), "license.bin");
            if (File.Exists(binPath))
            {
                var lines = File.ReadAllLines(binPath);
                return lines.Length > 0 ? lines[0] : string.Empty;
            }
            return string.Empty;
        }

        public static PhysicalAddress GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                //nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                if (nic.OperationalStatus.Equals(OperationalStatus.Up))
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
    }
}