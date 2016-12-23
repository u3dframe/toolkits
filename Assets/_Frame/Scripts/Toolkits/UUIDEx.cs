using UnityEngine;

/// <summary>
/// 客户端唯一标识工具
/// Ancher : Canyon
/// Create : 2016-03-21 15:16
/// </summary>
namespace Toolkits
{
    using System.Net.NetworkInformation;

	public static class UUIDEx
    {
        // 唯一标识
        static string cacheUUID = "";
        static string cacheUUIDMD5 = "";
        static string cacheUID = "";
        static string cacheUIDMD5 = "";

        static public string getMacAddress()
        {
            string macAdress = "";
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                PhysicalAddress address = adapter.GetPhysicalAddress();
                macAdress = address.ToString();
                if (!string.IsNullOrEmpty(macAdress))
                {
                    return macAdress;
                }
            }
            return "00";
        }

        static public string uuid {
            get
            {
                if (string.IsNullOrEmpty(cacheUUID))
                {
                    if (Application.platform == RuntimePlatform.Android ||
                        Application.platform == RuntimePlatform.IPhonePlayer)
                    {
                        cacheUUID = SystemInfo.deviceUniqueIdentifier;
                    }
                    else
                    {
                        cacheUUID = getMacAddress();
                    }
                }
                return cacheUUID;
            }
        }

        static public string uuidMd5
        {
            get
            {
                if (string.IsNullOrEmpty(cacheUUIDMD5))
                {
                    cacheUUIDMD5 = MD5Ex.encrypt(uuid);
                }
                return cacheUUIDMD5;
            }
        }

        static public string uid
        {
            get
            {
                if (string.IsNullOrEmpty(cacheUID))
                {
                    cacheUID = Application.platform + "_" + uuid;
                }
                return cacheUID;
            }
        }

        static public string uidMd5
        {
            get
            {
                if (string.IsNullOrEmpty(cacheUIDMD5))
                {
					cacheUIDMD5 = MD5Ex.encrypt(uid); 
                }
                return cacheUIDMD5;
            }
        }

		static public string newUid
		{
			get
			{
				return uid + "_" + System.DateTime.Now.Ticks;
			}
		}
		
		static public string newUidMd5
		{
			get
			{
				return MD5Ex.encrypt(newUid); 
			}
		}
    }
}