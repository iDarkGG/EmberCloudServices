using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
 
namespace EmberCloudServices
{
    public class HashCreator
    {
        private byte[] tmpStore;
        private byte[] hash;
        private string finalhash;
        private string finalhash2;
        private SHA256 sha256;
 
        public  string FinalHash { get => finalhash; }
 
        public bool EncryptPassword(string stringtohash)
        {
            tmpStore = Encoding.ASCII.GetBytes(stringtohash);
            sha256 = SHA256.Create();
            hash = sha256.ComputeHash(tmpStore);
            var sOutput = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {
                sOutput.Append(hash[i].ToString("X2"));
            }
            finalhash = sOutput.ToString();
 
            return true;
        }
 
        public bool CompareHash(string hash1, string hash2)
        {
            if (hash1.Equals(hash2))
            {
                return true;
            }
            return false;
        }
 

 
    }
}
