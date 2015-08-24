using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace MerlinClient.Controller
{
    class BaseNetwork
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        /// <summary>  
        /// SendArp获取MAC地址  
        /// </summary>  
        public static string GetMacAddress(string remoteIP)
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                Int32 remote = inet_addr(remoteIP); 
                Int64 macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);
                string temp = System.Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();
                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5) { strReturn.Append(temp.Substring(x - 2, 2)); }
                    else { strReturn.Append(temp.Substring(x - 2, 2) + ":"); }
                    x -= 2;
                }
                return strReturn.ToString();
            }
            catch
            {
                return "";
            }
        }  

        /// <summary>
        /// 得到本机IP
        /// </summary>
        public static string GetLocalIP()
        {
            string strLocalIP = "";
            string strPcName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strPcName);
            foreach (var IPadd in ipEntry.AddressList)
            {
                if (IsRightIP(IPadd.ToString()))
                {
                    strLocalIP = IPadd.ToString();
                    break;
                }
            }
            return strLocalIP;
        }

        /// <summary>
        /// 得到网关地址
        /// </summary>
        public static string GetGateway()
        {
            string strGateway = "";
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var netWork in nics)
            {
                IPInterfaceProperties ip = netWork.GetIPProperties();
                GatewayIPAddressInformationCollection gateways = ip.GatewayAddresses;
                foreach (var gateWay in gateways)
                {
                    if (IsPingIP(gateWay.Address.ToString()))
                    {
                        strGateway = gateWay.Address.ToString();
                        break;
                    }
                }

                if (strGateway.Length > 0)
                {
                    break;
                }
            }

            return strGateway;
        }

        /// <summary>
        /// 判断是否为正确的IP地址
        /// </summary>
        /// <param name="strIPadd">需要判断的字符串</param>
        /// <returns>true = 是 false = 否</returns>
        public static bool IsRightIP(string strIPadd)
        {
            if (Regex.IsMatch(strIPadd, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 尝试Ping指定IP是否能够Ping通
        /// </summary>
        /// <param name="strIP">指定IP</param>
        /// <returns>true 是 false 否</returns>
        public static bool IsPingIP(string strIP)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(strIP, 1000);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
