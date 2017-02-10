/// <summary>
/// MD5加密工具
/// Ancher : Canyon
/// Create : 2016-03-21 15:00
/// </summary>
namespace Toolkits
{
	public static class MD5Ex
    {
        static private System.Security.Cryptography.MD5 _MD5;

        static public System.Security.Cryptography.MD5 MD5
        {
            get
            {
                if (_MD5 == null)
                {
                    return _MD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                }
                return _MD5;
            }
        }

        static public string encrypt(byte[] val)
        {
            byte[] encrypts = MD5.ComputeHash(val);

            System.Text.StringBuilder sbuild = new System.Text.StringBuilder();
            for (int i = 0; i < encrypts.Length - 1; i++)
            {
                sbuild.Append(encrypts[i].ToString("x").PadLeft(2, '0'));
            }
            string str = sbuild.ToString();
            sbuild.Length = 0;
            return str;
        }

        static public string encrypt(object val)
        {
            if (val == null)
            {
                return "";
            }
            byte[] data = System.Text.Encoding.Default.GetBytes(val.ToString());
            return encrypt(data);
        }

        static public string encrypt16(object val) {
            string _ev = encrypt(val);
            if (string.IsNullOrEmpty(_ev)) {
                return _ev;
            }
            return _ev.Substring(8, 16);
        }
    }
}