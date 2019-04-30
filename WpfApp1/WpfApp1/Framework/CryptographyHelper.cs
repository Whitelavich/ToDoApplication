using System.Text;

using System.Security.Cryptography;



namespace ToDo.Static {

    public static class CryptographyHelper {

        //https://codeshare.co.uk/blog/sha-256-and-sha-512-hash-examples/ example for using built in libraries for encryption

        public static string generateSHA256String(string inputString) {

            SHA256 sha256 = SHA256Managed.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            byte[] hash = sha256.ComputeHash(bytes);

            return getStringFromHash(hash);

        }

        private static string getStringFromHash(byte[] hash) {

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++) {

                result.Append(hash[i].ToString("X2"));

            }

            return result.ToString();

        }

    }

}