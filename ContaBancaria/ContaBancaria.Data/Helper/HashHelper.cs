using System.Security.Cryptography;
using System.Text;

namespace ContaBancaria.Data.Helper
{
    public static class HashHelper
    {
        public static string GerarSha1(string texto)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(texto));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
