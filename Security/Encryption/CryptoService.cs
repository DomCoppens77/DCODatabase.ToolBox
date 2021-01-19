using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DCODatabase.ToolBox.Security.Encryption
{
    public class CryptoService
    {
        //CryptoService sc = new CryptoService();
        //string pubKey = sc.GetPublicKey();
        //Console.WriteLine("pub");
        //    Console.WriteLine(pubKey);
        //    Console.ReadKey();

        //    CryptoService sc2 = new CryptoService(pubKey);

        //string original = "salut";

        //Console.WriteLine("Original :" + original);

        //    byte[] Crypted = sc2.Crypt(original);

        //Console.WriteLine("crypted :" + Encoding.UTF8.GetString(Crypted));

        //    string uCrypted = sc.UnCrypt(Crypted);

        //Console.WriteLine("Uncrypted :" + uCrypted);
        //    Console.ReadKey();

        private RSACryptoServiceProvider rsa;

        public CryptoService(int byteSize = 2048)
        {
            // Create Public Key & Private Jey
            rsa = new RSACryptoServiceProvider(byteSize);
        }
        public CryptoService(string pubKey)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pubKey);
        }

        public String GetPublicKey()
        {
            return rsa.ToXmlString(false);
        }

        public byte[] Crypt(string raw)
        {
            return rsa.Encrypt(Encoding.UTF8.GetBytes(raw), true);
        }

        public string UnCrypt(byte[] crypted)
        {
            try
            {
                return Encoding.UTF8.GetString(rsa.Decrypt(crypted, true));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
