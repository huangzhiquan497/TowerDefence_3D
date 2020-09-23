using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace GameData
{
    public class EncryptedJsonSaver<T> : JsonSaver<T> where T : IDataStore
    {
        public EncryptedJsonSaver(string fileName) : base(fileName)
        {
        }

        private const int InitializationVectorLength = 16;
        private const int KeyLength = 32;

        private readonly byte[] _salt =
        {
            0x6b, 0xb0, 0xa1, 0x65, 0x08, 0xf8, 0xe6, 0xe8, 0x4d, 0x9e, 0x2f, 0x19, 0x97, 0xec, 0x0d, 0x6e,
            0xe7, 0xec, 0xe2, 0x0a, 0xd9, 0x47, 0xa7, 0x8d, 0xff, 0x3d, 0xe1, 0x65, 0x4f, 0x46, 0x00, 0x22
        };

        private byte[] GetUniqueDeviceBytes => Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);

        protected override StreamWriter GetWriteStream()
        {
            var fileStream = new FileStream(_fileName, FileMode.Create);

            var byteGenerator = new Rfc2898DeriveBytes(GetUniqueDeviceBytes, _salt, 1000);
            var random = new RNGCryptoServiceProvider();
            var key = byteGenerator.GetBytes(KeyLength);
            var iv = new byte[InitializationVectorLength];
            random.GetBytes(iv);

            var rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;

            fileStream.Write(iv, 0, InitializationVectorLength);
            var encryptedSteam = new CryptoStream(fileStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);

            return new StreamWriter(encryptedSteam);
        }

        protected override StreamReader GetReadStream()
        {
            var fileStream = new FileStream(_fileName, FileMode.Open);

            var byteGenerator = new Rfc2898DeriveBytes(GetUniqueDeviceBytes, _salt, 1000);
            
            var key = byteGenerator.GetBytes(KeyLength);
            var iv = new byte[InitializationVectorLength];
            
            fileStream.Read(iv, 0, InitializationVectorLength);
            
            var rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = iv;

            
            var encryptedSteam = new CryptoStream(fileStream, rijndael.CreateDecryptor(), CryptoStreamMode.Read);

            return new StreamReader(encryptedSteam);
        }
    }
}